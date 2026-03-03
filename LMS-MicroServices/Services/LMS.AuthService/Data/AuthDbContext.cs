using LMS.AuthService.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.AuthService.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles with fixed GUIDs
            var roles = new[]
            {
                new IdentityRole 
                { 
                    Id = "11111111-1111-1111-1111-111111111111", 
                    Name = "Admin", 
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "11111111-1111-1111-1111-111111111111"
                },
                new IdentityRole 
                { 
                    Id = "22222222-2222-2222-2222-222222222222", 
                    Name = "User", 
                    NormalizedName = "USER",
                    ConcurrencyStamp = "22222222-2222-2222-2222-222222222222"
                },
                new IdentityRole 
                { 
                    Id = "33333333-3333-3333-3333-333333333333", 
                    Name = "Instructor", 
                    NormalizedName = "INSTRUCTOR",
                    ConcurrencyStamp = "33333333-3333-3333-3333-333333333333"
                },
                new IdentityRole 
                { 
                    Id = "44444444-4444-4444-4444-444444444444", 
                    Name = "Student", 
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = "44444444-4444-4444-4444-444444444444"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
