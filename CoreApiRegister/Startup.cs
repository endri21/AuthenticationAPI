using CoreApiRegister.Data;
using CoreApiRegister.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CoreApiRegister.Infrastructure;

namespace CoreApiRegister
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                .UseSqlServer(
                   this.Configuration.GetConnectionString("DefaultConnection")));
            services.
                AddDatabaseDeveloperPageExceptionFilter();

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



                //(options =>
                //options
                //.SignIn
                //.RequireConfirmedAccount = false) //confirmation
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //serialize the user
            var appsettingsConfig = this.Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appsettingsConfig);

            var appsetings = appsettingsConfig.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appsetings.Secret);

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


            services
                .AddControllers();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseMigrationsEndPoint();
            }



            app.UseRouting();
            //will deployed in another server 
            app.UseCors(op => op
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            app.ApplyMigrations();
        }
    }
}
