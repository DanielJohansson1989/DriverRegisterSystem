using DriverRegisterSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace DriverRegisterSystem.Data
{
    public class SeedData
    {
        public static async Task Initialize(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Check if Admin role exists, if not, create it
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Check if Employee role exists, if not, create it
            if (!await roleManager.RoleExistsAsync("Employee"))
            {
                await roleManager.CreateAsync(new IdentityRole("Employee"));
            }

            // Create the default Admin user if not exists
            var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminUser == null)
            {
                var user = new Employee
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    Name = "Admin Daniel"
                };

                var result = await userManager.CreateAsync(user, "AdminPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
