using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesAPI.Models;

namespace CitiesAPI.DTOs
{
    public class CityDTO
    {
        
        public string Name { get; set; }

        
        public int Id { get; set; }

        
        public string Description { get; set; }

        public List<Attraction> Attractions { get; set; }
    }
}
