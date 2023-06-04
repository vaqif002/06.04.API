﻿using HomeWork1.DAL;
using HomeWork1.Entities;
using HomeWork1.Entities.Dtos.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetAllCars()
        {
            var result = await _context.Cars.ToListAsync();
            if (result.Count == 0) { return NotFound(); }
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCarDto createCarDto)
        {
            Car car = new Car
            {
                BrandId = createCarDto.BrandId,
                ColorId = createCarDto.ColorId,
                ModelYear = DateTime.UtcNow,
                DailyPrice = createCarDto.DailyPrice,
                Description = createCarDto.Description,
            };
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarDto update)
        {
            var result = await _context.Cars.FindAsync(update.Id);
            if (result is null)
            {
                return NotFound();
            }
            result.BrandId = update.BrandId;
            result.ColorId = update.ColorId;
            result.DailyPrice = update.DailyPrice;
            result.Description = update.Description;

            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Cars.FindAsync(id);
            if (result is null) { return NotFound(); }
            _context.Cars.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cars = _context.Cars.Find(id);
            if (cars == null)
            {
                return NotFound();
            }

            return Ok(cars);
        }
    } 

}
