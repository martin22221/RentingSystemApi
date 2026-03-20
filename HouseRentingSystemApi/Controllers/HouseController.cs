using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemApi.Controllers
{
    [Route("api/[controller")]
    public class HouseController : ControllerBase
    {
        private AppDbContext context;

        public HouseController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var model = context.Houses
                .Select(h => new HouseDetailModel()
                {
               
                    Title = h.Title,
                    Address = h.Address,

                })
                .ToList();
          return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id )
        {
            return Ok();
        }
        [HttpPost("All")] 
        public IActionResult All(HouseDetailModel model)
        {
            if(ModelState.IsValid)
            {
                return BadRequest();
            }
            context.Houses.Add(new House()
            {
                Title = model.Title,
                Address = model.Address,
                Description = "TestDescription",
                ImageUrl = model.ImageUrl,
                PricePerMonth = 100m,
                CategoryId = 1

            });
            context.SaveChanges();
            return Ok();
        }
    }
}
