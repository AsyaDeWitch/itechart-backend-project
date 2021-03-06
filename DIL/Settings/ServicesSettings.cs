using BLL.Services;
using BLL.Interfaces;
using DAL.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using DIL.Handlers;
using DIL.Requirements;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DAL;
using RIL.Models;
using AutoMapper;
using DIL.ActionFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using BLL.Cachers;
using DAL.Interfaces;
using DAL.Repositories;
using BLL.Converters;

namespace DIL.Settings
{
    public class ServicesSettings
    {
        public static void InjectDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ExtendedUser, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddMemoryCache();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                policy.Requirements.Add(new RoleAuthorizationRequirement("Admin")));
                options.AddPolicy("RequireUserRole", policy =>
                policy.Requirements.Add(new RoleAuthorizationRequirement("User")));
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingSettings());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler,
                          RoleAuthorizationMiddlewareResultHandler>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IValidatorService, ValidatorService>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
            services.AddScoped<IProductRatingRepository, ProductRatingRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IExtendedUserRepository, ExtendedUserRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            services.AddScoped<IProductOrderConverter, ProductOrderConverter>();
            services.AddScoped<IProductRatingConverter, ProductRatingConverter>();
            services.AddScoped<IOrderConverter, OrderConverter>();
            services.AddScoped<IProductConverter, ProductConverter>();
            services.AddScoped<IAddressConverter, AddressConverter>();
            services.AddScoped<IUserConverter, UserConverter>();
            services.AddScoped<IConverter, Converter>();

            services.AddScoped<IMemoryCacher, MemoryCacher>();

            services.AddScoped<IUserClaimsPrincipalFactory<ExtendedUser>, ExtendedUserClaimsPrincipalFactory>();
            
            services.AddScoped<SortAndFilterParamsValidationActionFilter>();
            services.AddScoped<ProductValidationActionFilter>();
            services.AddScoped<OrderAndProductsValidationActionFilter>();

            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }
    }
}
