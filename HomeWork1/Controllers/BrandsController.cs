using HomeWork1.DAL;
using HomeWork1.Entities;
using HomeWork1.Entities.Dtos.Brand;
using HomeWork1.Entities.Dtos.Color;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : Controller
    {
        private readonly AppDbContext _context;

        public BrandsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetAllColors()
        {
            var result = await _context.Brands.ToListAsync();
            if (result.Count == 0) { return NotFound(); }
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateBrandDto createBrandDto)
        {
            Brand brands = new Brand
            {
                Name = createBrandDto.Name,
            };
            await _context.Brands.AddAsync(brands);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBrandDto updateBrandDto)
        {
            var result = await _context.Brands.FindAsync(updateBrandDto.Id);
            if (result is null)
            {
                return NotFound();
            }
            result.Id = updateBrandDto.Id;
            result.Name = updateBrandDto.Name;


            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Brands.FindAsync(id);
            if (result is null) { return NotFound(); }
            _context.Brands.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brands = _context.Brands.Find(id);
            if (brands == null)
            {
                return NotFound();
            }

            return Ok(brands);
        }
    }
}

