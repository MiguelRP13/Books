using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Books.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Books.Data
{
    public class UsersSeedData
    {
        public static async Task EnsurePopulatedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole>roleManager)
        {
            const string adminName = "Admin";
            const string adminPass = "Secret 123$";
            const string customerName = "john";
            const string customerPass = "Secret 123$";


            ApplicationUser admin = await userManager.FindByNameAsync(adminName);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminName };
                await userManager.CreateAsync(admin, adminPass);
            }
            ApplicationUser customer = await userManager.FindByNameAsync(customerName);
            if (customer == null)
            {
                customer = new ApplicationUser { UserName = customerName };
                await userManager.CreateAsync(customer, customerPass);
            }

            if(!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (!await userManager.IsInRoleAsync(admin, "Administrator"))
            {
                await userManager.AddToRoleAsync(admin, "Administrator");
            }

            if (!await userManager.IsInRoleAsync(customer, "Customer"))
            {
                await userManager.AddToRoleAsync(customer, "Customer");
            }

        }
    }
}
