using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using DIL.Settings;
using Web.Extensions;
using System.IO;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Linq;
using System;
using System.Net.Http;
using DIL.HealthChecks;
using HealthChecks.UI.Client;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAndValidate<JwtSettings>(Configuration);

            //Register application dependencies
            ServicesSettings.InjectDependencies(services, Configuration);
            services.AddDatabaseDeveloperPageExceptionFilter();

            //Register AutoMapper
            services.AddAutoMapper(typeof(Startup).Assembly);

            //Register required services for health checks
            services.AddHealthChecks()
                .AddSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);

            //Register the Swagger generator
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "LabWebApp API V1",
                            Version = "v1"
                        }
                    );
                    var currentAssembly = Assembly.GetExecutingAssembly();
                    var xmlDocs = currentAssembly.GetReferencedAssemblies()
                    .Union(new AssemblyName[] { currentAssembly.GetName() })
                    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                    .Where(f => File.Exists(f)).ToArray();
                    Array.ForEach(xmlDocs, (d) =>
                    {
                        c.IncludeXmlComments(d);
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization cookie using the Bearer scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Cookie,
                        Type = SecuritySchemeType.ApiKey
                    });
                });

            // Register required services for health checks
            services.AddHealthChecks()
                .AddCheck(
                    "DbConnection-check",
                    new SqlConnectionHealthCheck(Configuration["ConnectionStrings:DefaultConnection"]),
                    HealthStatus.Unhealthy,
                    new string[] {"dbconnection"});
                //.AddSqlServer(Configuration["ConnectionStrings:DefaultConnection"], failureStatus: HealthStatus.Unhealthy);

            services.AddHealthChecksUI(setup => 
            {
                setup.UseApiEndpointHttpMessageHandler(sp =>
                {
                    return new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
                    };
                });
            })
                .AddInMemoryStorage();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //Captures synchronous and asynchronous Exception instances from the pipeline and generates HTML error responses.
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies["JwtToken"];
                if (!string.IsNullOrEmpty(token))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    AllowCachingResponses = false,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI(config => config.UIPath = "/healthchecks-ui");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LabWebApp API V1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHealthChecksUI(config => config.UIPath = "/healthchecks-ui");
        }
    }
}
