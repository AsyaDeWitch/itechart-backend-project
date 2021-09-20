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

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //appsettings.json parameters validation
            services.ConfigureAndValidate<JwtSettings>(Configuration);

            ServicesSettings.InjectDependencies(services, Configuration);
            services.AddDatabaseDeveloperPageExceptionFilter();

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
            services.AddHealthChecksUI()
                .AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksUI();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    AllowCachingResponses = false,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LabWebApp API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHealthChecksUI(config => config.UIPath = "/healthchecks-ui");
        }
    }
}
