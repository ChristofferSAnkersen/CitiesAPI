using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesAPI.Models;
using CitiesAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitiesAPI.Controllers
{
    [Produces("application/xml", "application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {

        private readonly DataContext _context;
        private List<CityDTO> citiesDto = new List<CityDTO>();
        private List<CityWithoutAttractionsDTO> citiesWithoutAttractionsDto = new List<CityWithoutAttractionsDTO>();


        public CitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        //[Route("cities")]
        public IActionResult GetCities(bool shouldReturnAttractions = false)
        {
            List<City> cities = _context.Cities.ToList();

            foreach (var city in cities)
            {
                var _citiesDto = new CityDTO();
                _citiesDto.Id = city.Id;
                _citiesDto.Name = city.Name;
                _citiesDto.Description = city.Description;
                _citiesDto.Attractions = city.Attractions;
                citiesDto.Add(_citiesDto);
            };

            if (!shouldReturnAttractions)
            {
                foreach (var city in cities)
                {
                    var _citiesWithoutAttractionsDto = new CityWithoutAttractionsDTO();
                    _citiesWithoutAttractionsDto.Id = city.Id;
                    _citiesWithoutAttractionsDto.Name = city.Name;
                    _citiesWithoutAttractionsDto.Description = city.Description;
                    citiesWithoutAttractionsDto.Add(_citiesWithoutAttractionsDto);
                }
                
                return new ObjectResult(citiesWithoutAttractionsDto);
            }

            return new ObjectResult(citiesDto);
            //return new ObjectResult(_context.Cities.Include(x => x.Attractions).ToList());
        }

        [HttpGet("{id}")]
        [Authorize]
        //[Route("cities/{id}")]
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

            return new OkObjectResult(_context.Cities.Where(x => x.Id == id).Include(x => x.Attractions).ToList());
        }
        
        [HttpPost]
        //[Route("create")]
        public IActionResult CreateCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cities.Add(city);
            try
            {
                _context.SaveChanges();
                return CreatedAtAction("CreateCity", city);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            //return CreatedAtAction("CreateCity", city);
        }

        [HttpDelete("{id}")]
        //[Route("delete/{id}")]
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

        [HttpPut("city")]
        //[Route("update")]
        public IActionResult Update(City city)
        {
            List<City> cities = _context.Cities.ToList();

            if (!cities.Exists(x => x.Id == city.Id))
            {
                return NotFound();
            }

            City oldCity = _context.Cities.FirstOrDefault(x => x.Id == city.Id);
            if (oldCity == null)
            {
                return NotFound();
            }
            _context.Entry(oldCity).CurrentValues.SetValues(city);
            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
            

            
        }

        [HttpPatch("{id}")]
        //[Route("update/{id}")]
        public IActionResult Patch(JsonPatchDocument<City> cityUpdate, int id)
        {
            City oldCity = _context.Cities.FirstOrDefault(x => x.Id == id);
            if (oldCity == null)
            {
                return NotFound();
            }

            cityUpdate.ApplyTo(oldCity);
            _context.Update(oldCity);
            _context.SaveChanges();

            return Ok();
        }
    }
}
