using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kkmlsfi_ns.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "FD6D3EE9-F178-4636-9D87-21995CB1454B";
            var writerRoleId = "58E7FC40-C947-4693-8E81-FB609C07E30C";

            //Create roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //Seed roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create Admin user
            var adminUserId = "3A4007F6-4341-4EFB-B837-11F53BD7BC42";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@kkmlsfi.com",
                Email = "admin@kkmlsfi.com",
                NormalizedEmail = "admin@kkmlsfi.com".ToUpper(),
                NormalizedUserName = "admin@kkmlsfi.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin");

            builder.Entity<IdentityUser>().HasData(admin);

            //Give roles to Admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
