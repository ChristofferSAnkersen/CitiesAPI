using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.Models
{
    public class City
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public List<Attraction> Attractions { get; set; }

    }
}
