using Hospital_Management_System.DbContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;



namespace Hospital_Management_System.Models
{
    public static class SeedData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var _userManager =
                         serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var _roleManager =
                         serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (!context.Users.Any(usr => usr.UserName == "2018cs73@student.uet.edu.com"))
                {
                    var user = new IdentityUser()
                    {
                        UserName = "2018cs73@student.uet.edu.com",
                        Email = "2018cs73@student.uet.edu.com",
                        EmailConfirmed = true,
                    };

                    var userResult = _userManager.CreateAsync(user, "P@ssw0rd").Result;
                }

                if (!_roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = _roleManager.CreateAsync
                               (new IdentityRole { Name = "Admin" }).Result;
                }

                var adminUser = _userManager.FindByNameAsync("2018cs73@student.uet.edu.com").Result;
                var userRole = _userManager.AddToRolesAsync
                               (adminUser, new string[] { "Admin" }).Result;

                context.SaveChanges();
            }
        }
    }
}
