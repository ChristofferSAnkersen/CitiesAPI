using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.Models
{
    public class City
    {
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public int Id { get; set; }

        [StringLength(1200)]
        public string Description { get; set; }

        public List<Attraction> Attractions { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
