using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.Models
{
    public class Attraction
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(1200)]
        public string Description { get; set; }

        [ForeignKey("Id")]
        public int CityId { get; set; }
    }
}
