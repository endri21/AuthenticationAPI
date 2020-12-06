
using CoreApiRegister.Data;
using CoreApiRegister.Data.Models;
using CoreApiRegister.Features.Companies;
using CoreApiRegister.Features.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CoreApiRegister.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {

        //konfigurimi i secret key 
        public static AppSettings GetApplicatinSettings(
          this IServiceCollection services,
            IConfiguration configuration)
        {
            var appsettingsConfig = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appsettingsConfig);
            var appsetings = appsettingsConfig.Get<AppSettings>();
            return appsetings;
        }


        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<ApplicationDbContext>(options =>
                options
                .UseSqlServer(configuration
                    .GetDefaultConnectionString()));


        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.
           AddIdentity<User, IdentityRole>(opt =>
           {
               opt.Password.RequiredLength = 6;
               opt.Password.RequireDigit = false;
               opt.Password.RequireLowercase = false;
               opt.Password.RequireNonAlphanumeric = false;
               opt.Password.RequireUppercase = false;
           }
           )
            .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }

        //per authentikim

        //serialize the user
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;

        }



        //lidhja ndermjet repository , interface dhe controller 
        //ose ne kete rast features service and interface 
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)

          => services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ICompaniesService, CompaniesService>();


        public static IServiceCollection AddSwagger(this IServiceCollection services)
         => services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo { 
                            Title = "Core register API",
                            Version = "v1"
                        });
                });
      


    }
}
