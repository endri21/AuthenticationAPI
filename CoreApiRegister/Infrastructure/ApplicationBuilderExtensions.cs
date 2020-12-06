

using CoreApiRegister.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApiRegister.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        {
          return  app
             .UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core register API");
                 c.RoutePrefix = string.Empty;
             });
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbcontext =  services.ServiceProvider.GetService<ApplicationDbContext>();
            dbcontext.Database.Migrate();
        }
    }
}
