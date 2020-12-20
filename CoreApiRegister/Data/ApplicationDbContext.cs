using CoreApiRegister.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CoreApiRegister.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


       
        public  DbSet<Company> Companies { get; set; } 
        public  DbSet<UserPost> UserPost { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Company>()
                .HasOne(u=>u.user)
                .WithMany(u=>u.Companies)
                .HasForeignKey(u=>u.UserId)
                .OnDelete(DeleteBehavior.Restrict);  
            base.OnModelCreating(builder);
        }
    }
}
