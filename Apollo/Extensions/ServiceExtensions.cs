using System.Text;
using Apollo.CustomFormatter;
using Apollo.Presentation.Controllers;
using AspNetCoreRateLimit;
using Contracts;
using Entities.Models;
using LoggerService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using Service.Contracts;

namespace Apollo.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
    });


    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => { });

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlClient(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Apollo")));

    public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder) =>
        builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

    public static void AddCustomMediaTypes(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(config =>
        {
            var systemTextJsonOutputFormatter = config.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

            systemTextJsonOutputFormatter?.SupportedMediaTypes
                .Add("application/vnd.apollo.hateoas+json");

            systemTextJsonOutputFormatter?.SupportedMediaTypes
                .Add("application/vnd.apollo.apiroot+json");

            var xmlOutputFormatter = config.OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?
                .FirstOrDefault();

            xmlOutputFormatter?.SupportedMediaTypes
                .Add("application/vnd.apollo.hateoas+xml");

            xmlOutputFormatter?.SupportedMediaTypes
                .Add("application/vnd.apollo.apiroot+xml");
        });
    }

    public static void ConfigureVersioning(this IServiceCollection services) =>
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            opt.Conventions.Controller<CompaniesController>()
                .HasApiVersion(new ApiVersion(1, 0));
            opt.Conventions.Controller<CompaniesV2Controller>().HasDeprecatedApiVersion(new ApiVersion(2, 0));
        });

    public static void ConfigureResponseCaching(this IServiceCollection services)
        => services.AddResponseCaching();

    public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        => services.AddHttpCacheHeaders(
            (expirationOpt) =>
            {
                expirationOpt.MaxAge = 650;
                expirationOpt.CacheLocation = CacheLocation.Private;
            },
            (validationOpt)
                => { validationOpt.MustRevalidate = true; });

    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        var rateLimitRules = new List<RateLimitRule>
        {
            new()
            {
                Endpoint = "*",
                Limit = 30,
                Period = "5m"
            }
        };

        services.Configure<IpRateLimitOptions>(opt => { opt.GeneralRules = rateLimitRules; });
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
    }
    
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSetting = configuration.GetSection("JwtSetting");
        var secretKey = Environment.GetEnvironmentVariable("SECRETKEY");

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSetting["validIssuer"],
                    ValidAudience = jwtSetting["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
    }
    
}