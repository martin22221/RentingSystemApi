using HouseRentingSystemApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemApi.Controllers
{
    [Route("api/[controller")]
    public class HouseController : ControllerBase
    {
        private List<HouseDetailModel> houses = new List<HouseDetailModel>()
        {

          new HouseDetailModel()
          {
              Id = 1,
            Name = "House on the beach",
            Address = "Florida,Miami",
           ImageUrl = @"https://images.mansionglobal.com/im-73534380"
          },

          new HouseDetailModel()
          {
              Id = 2,
            Name = "Mountain house",
            Address = "Rila Mountain",
           ImageUrl = @"https://static.workaway.info/gfx/foto/5/3/4/9/9/534999836525/xl/534999836525_148985760506288.jpg"
          },

          new HouseDetailModel()
          {
              Id = 3,
            Name = "House ",
            Address = "Sofia,Lulin",
           ImageUrl = @"https://www.alo.bg/user_files/n/nedvijimiimotipernikeood/10694713_141151873_medium.jpg"
          }

        };
    
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
          return Ok(houses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id )
        {
            return Ok(houses.FirstOrDefault(h => h.Id == id));
        }
    }
}
