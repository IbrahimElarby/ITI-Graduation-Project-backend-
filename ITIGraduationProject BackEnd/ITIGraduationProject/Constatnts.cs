using Microsoft.AspNetCore.Identity;

namespace BugProject
{
    public static class Constatnts
    {
        public static class Policies
        {
            public const string ForAdminOnly = "ForAdminOnly";
            public const string ForDev = "ForDev";
            public const string ForTester = "ForTester";
        }

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            string[] roleNames = { "ADMIN", "MANAGER", "USER" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }
        }
    }

    enum BugRoles
    {
        Manager,
        Developer,
        Tester,
        PreimumUser,
        User
    }
}
