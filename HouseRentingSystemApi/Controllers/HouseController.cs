using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemApi.Controllers
{
    [ApiController]
    [Route("api/house")]
    public class HouseController : ControllerBase
    {
        private readonly AppDbContext context;

        public HouseController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/house/all
        [HttpGet("all")]
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

        // GET: api/house/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await context.Houses
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id);

            if (house == null)
                return NotFound();

            return Ok(new HouseDetailModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                Description = house.Description,
                PricePerMonth = house.PricePerMonth
            });
        }

        // POST: api/house
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HouseDetailModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newHouse = new House()
            {
                Title = model.Title,
                Address = model.Address,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                PricePerMonth = model.PricePerMonth
            };

            var category = await context.Categories
                .FirstOrDefaultAsync(c => c.Name == model.Category.ToString());

            if (category == null)
            {
                var newCategory = new Category()
                {
                    Name = model.Category.ToString()
                };

                context.Categories.Add(newCategory);
                await context.SaveChangesAsync();

                newHouse.CategoryId = newCategory.Id;
            }
            else
            {
                newHouse.CategoryId = category.Id;
            }

            context.Houses.Add(newHouse);
            await context.SaveChangesAsync();

            return Ok(newHouse);
        }

        // PUT: api/house/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] HouseDetailModel model)
        {
            var house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);

            if (house == null)
                return NotFound();

            house.Title = model.Title;
            house.Address = model.Address;
            house.ImageUrl = model.ImageUrl;
            house.Description = model.Description;
            house.PricePerMonth = model.PricePerMonth;

            await context.SaveChangesAsync();

            return Ok(house);
        }

        // DELETE: api/house/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);

            if (house == null)
                return NotFound();

            context.Houses.Remove(house);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}