using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CitiesAPI.Models
{
    public class DataContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Attraction> Attractions { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
