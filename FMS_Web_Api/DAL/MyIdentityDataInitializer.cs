using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.DAL
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedRoles
(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
        ("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";                
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
        ("PMO").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "PMO";                
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync
        ("PMC").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "PMC";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }

        public static void SeedUsers
(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync
        ("admin").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";

                IdentityResult result = userManager.CreateAsync
                (user, "Admin@123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Admin").Wait();
                }
            }

        }
        public static void SeedData
        (UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

    }
}
