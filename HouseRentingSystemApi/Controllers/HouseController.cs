using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystemApi.Controllers
{
    [Route("api/[controller]")]
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
        [Produces(typeof(HouseDetailModel))]
        [Authorize]
        public IActionResult All([FromBody  ] HouseDetailModel model)
        {
            if(ModelState.IsValid)
            {
                return BadRequest();
            }
            var house = new House()
            {   
                Title = model.Title,
                Address = model.Address,
                Description = "TestDescription",
                ImageUrl = model.ImageUrl,
                PricePerMonth = 100m,
                CategoryId = 1

            };
            context.Houses.Add(house);      
            context.SaveChanges();

            return Created($"api/All/{house.Id}",house);
        }
    }
}
