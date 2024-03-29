
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoreApiRegister.Infrastructure;
using CoreApiRegister.Infrastructure.Extensions;
using CoreApiRegister.Infrastructure.Filters;

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
       => services
            .AddDatabase(this.Configuration)
                .AddIdentity()
                .AddJwtAuthentication(services.GetApplicatinSettings(this.Configuration))
                .AddApplicationServices()
                .AddSwagger()
                .AddApiCotroller();
               

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseMigrationsEndPoint();
            }



         app
            .UseSwaggerUI()
            .UseRouting()
            .UseCors(op => op
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            })
            .ApplyMigrations();
        }
    }
}
