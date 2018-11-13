using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.DTOs
{
    public class CityWithoutAttractionsDTO
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

    }
}
