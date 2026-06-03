using HouseRentingSystemApi.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystemApi.Services
{
    public class RoleService : IRoleService
    {
        public async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Agent", "Client" };

            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}