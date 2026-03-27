using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystemApi.Models.Authorization
{
    public class Login
    {
        [Required]
        public required  string Username { get; set; }

        [Required]
        public required string Password { get; set; }

       
    }
}
