using HouseRentingSystemApi.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this.configuration = configuration;
        }

        public async Task <IActionResult> Register(string username, string password)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest(new
                {     
                    message = "Username already exist",
                });
            }

            //var token = GenerateJWTToken(user);
            //return Ok();
        }
    }
}
