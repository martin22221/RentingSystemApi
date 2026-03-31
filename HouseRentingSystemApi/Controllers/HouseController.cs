using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("All")]
        [Produces(typeof(IEnumerable<HouseDetailModel>))]
        public async Task<IActionResult> GetAll()
        {
            var model = await context.Houses
                .AsNoTracking()
                .Select(h => new HouseDetailModel()
                {

                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl
                })
                .ToListAsync();

            return Ok(model);
        }

        [HttpGet("{id}")]
        [Produces(typeof(HouseDetailModel))]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return Ok(new HouseDetailModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            });
        }

        [HttpPost]
        [Produces(typeof(HouseDetailModel))]
        public async Task<IActionResult> Create([FromBody] HouseDetailModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            var house = new House()
            {
                Title = model.Title,
                Address = model.Address,
                Description = "TesatDescription",
                ImageUrl = model.ImageUrl,
                PricePerMonth = 100m,
                CategoryId = 1
            };
            context.Houses.Add(house);
            await context.SaveChangesAsync();

            return Created($"api/{house.Id}", new HouseDetailModel() 
            {
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                Title = house.Title
            });
        }
    }
}