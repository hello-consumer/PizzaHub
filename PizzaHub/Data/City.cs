using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class City
    {
        public City()
        {
            Restaurant = new HashSet<Restaurant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Restaurant> Restaurant { get; set; }
    }
}
