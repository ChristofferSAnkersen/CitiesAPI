using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {

        private readonly DataContext _context;

        public CitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        [Route("Cities")]
        public IActionResult GetCities(bool shouldReturnAttractions = false)
        {
            List<City> cities = _context.Cities.ToList();
            if (!shouldReturnAttractions)
            {
                return new ObjectResult(cities);
            }

            return new ObjectResult(_context.Cities.Include(x => x.Attractions));
        }

        // GET: api/Cities/5
        [HttpGet]
        public IActionResult GetCity(int id, bool shouldReturnAttractions = false)
        {
            List<City> cities = _context.Cities.ToList();
            if (!cities.Exists(x => x.Id == id))
            {
                return NotFound();
            }

            if (!shouldReturnAttractions)
            {
                return new ObjectResult(cities.FindAll(x => x.Id == id).Select(x => new {x.Id, x.Name, x.Description}));
            }

            return new ObjectResult(_context.Cities.Where(x => x.Id == id).Include(x => x.Attractions));
        }

        // POST: api/Cities
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Cities.Add(city);
            _context.SaveChanges();
            return CreatedAtAction("CreateCity", city);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteCity(int id)
        {
            var city = _context.Cities.FirstOrDefault(x => x.Id == id);
            if (city is null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            _context.SaveChanges();
            return Ok();
        }

        public 

    }
}
