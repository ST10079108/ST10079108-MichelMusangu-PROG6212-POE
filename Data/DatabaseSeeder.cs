using Microsoft.AspNetCore.Identity;

namespace ClaimsPoe.Data
{
    public class DatabaseSeeder
    {
        public async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Define roles
            string[] roles = { "Lecturer", "HRManager" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
