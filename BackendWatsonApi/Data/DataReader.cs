using BackendWatsonApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;

namespace BackendWatsonApi.Data
{
    public class DataReader
    {
        public static async void Init(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var userstore = new UserStore<User>(context);

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var role = new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" };
                await roleStore.CreateAsync(role);
            }

            if (!context.User.Any(u => u.UserName == "admin@admin.com"))
            {
                //  This method will be called after migrating to the latest version.
                User user = new User
                {
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                var passwordHash = new PasswordHasher<User>();
                user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
                await userstore.CreateAsync(user);
                await userstore.AddToRoleAsync(user, "Administrator");
            }

        }
    }
}
