using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CitiesAPI.Controllers
{
    [Route("api/attractions")]
    [ApiController]
    public class AttractionsController : ControllerBase
    {
        private readonly DataContext _context;

        public AttractionsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Attractions
        [HttpGet]
        //[Route("attractions/{id}")]
        public IActionResult GetAttractions(int id)
        {
            var cities = _context.Cities.ToList();
            if (!cities.Exists(x => x.Id == id))
            {
                return NotFound();
            }

            return new OkObjectResult(_context.Attractions.Where(x => x.CityId == id));
        }

        // GET: api/attraction/id
        [HttpGet("{id}")]
        //[Route("attraction/{id}")]
        public IActionResult GetAttraction(int attractionId)
        {
            var attraction = _context.Attractions.ToList();
            if (!attraction.Exists(x => x.Id == attractionId))
            {
                return NotFound();
            }
            return new OkObjectResult(_context.Attractions.ToList());
        }


        [HttpGet]
        [Route("attractions")]
        public IActionResult GetAttractions()
        {
            var attractions = _context.Attractions.ToList();
            return new OkObjectResult(_context.Attractions.ToList());
        }

        [HttpPost]
        [Route("Create/{id}")]
        public IActionResult CreateAttraction(int id, Attraction attraction)
        {
            attraction.CityId = id;
            _context.Attractions.Add(attraction);
            _context.SaveChanges();

            return CreatedAtAction("CreateAttraction", attraction);
        }


        [HttpDelete]
        [Route("Delete/{attractionId}")]
        public IActionResult DeleteAttraction(int attractionId)
        {
            var attraction = _context.Attractions.FirstOrDefault(x => x.Id == attractionId);
            if (attraction == null)
            {
                return NotFound();
            }

            _context.Attractions.Remove(attraction);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(Attraction attraction)
        {
            var oldAttraction = _context.Attractions.FirstOrDefault(x => x.Id == attraction.Id);
            if (oldAttraction == null)
            {
                return NotFound();
            }

            _context.Entry(oldAttraction).CurrentValues.SetValues(attraction);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch]
        [Route("Update/{id}")]
        public IActionResult Patch(JsonPatchDocument<Attraction> attractionPatch, int id)
        {
            var oldAttraction = _context.Attractions.FirstOrDefault(x => x.Id == id);

            if (oldAttraction == null)
            {
                return NotFound();
            }

            attractionPatch.ApplyTo(oldAttraction);
            _context.Update(oldAttraction);
            _context.SaveChanges();

            return Ok();
        }


    }
}
