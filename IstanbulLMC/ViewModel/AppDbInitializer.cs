using IstanbulLMC.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace IstanbulLMC.ViewModel
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<lmcTourismContext>();
                context.Database.EnsureCreated();

            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(userRoles.admin))
                    await roleManager.CreateAsync(new IdentityRole(userRoles.admin));
                if (!await roleManager.RoleExistsAsync(userRoles.user))
                    await roleManager.CreateAsync(new IdentityRole(userRoles.user));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "Admin@istanbullms.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {

                        firstName = "Admin",
                        lastName = "User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    var createUserResult = await userManager.CreateAsync(newAdminUser, "Qpalzm@4321");
                    if (createUserResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdminUser, userRoles.admin);
                    }
                    else
                    {
                        // Log or handle the error here
                        foreach (var error in createUserResult.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                    }


                }


            }

        }
    }
}
