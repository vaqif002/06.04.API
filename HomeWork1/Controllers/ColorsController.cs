using HomeWork1.DAL;
using HomeWork1.Entities.Dtos.Car;
using HomeWork1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeWork1.Entities.Dtos.Color;

namespace HomeWork1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : Controller
    {
        private readonly AppDbContext _context;

        public ColorsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetAllColors()
        {
            var result = await _context.Colors.ToListAsync();
            if (result.Count == 0) { return NotFound(); }
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateColorDto createColor)
        {
            Color colors = new Color
            {
                Name = createColor.Name,
            };
            await _context.Colors.AddAsync(colors);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBrandDto updateColor)
        {
            var result = await _context.Colors.FindAsync(updateColor.Id);
            if (result is null)
            {
                return NotFound();
            }
            result.Id = updateColor.Id;
            result.Name=updateColor.Name;
            

            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Colors.FindAsync(id);
            if (result is null) { return NotFound(); }
            _context.Colors.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var colors = _context.Colors.Find(id);
            if (colors == null)
            {
                return NotFound();
            }

            return Ok(colors);
        }
    }
}
