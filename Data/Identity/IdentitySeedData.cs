using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class IdentitySeedData
    {
        public static async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            // Check if the roles already exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                // Create the "Admin" role
                var adminRole = new ApplicationRole { Name = "Admin" };
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                // Create the "Customer" role
                var customerRole = new ApplicationRole { Name = "Customer" };
                await roleManager.CreateAsync(customerRole);
            }
        }
    }
}
