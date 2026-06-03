using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystemApi.Services.Contracts
{
    public interface IRoleService
    {
        Task SeedRolesAsync(RoleManager<IdentityRole> roleManager);
    }
}