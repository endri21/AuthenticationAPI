

using CoreApiRegister.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApiRegister.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbcontext =  services.ServiceProvider.GetService<ApplicationDbContext>();
            dbcontext.Database.Migrate();
        }
    }
}
