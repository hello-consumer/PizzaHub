using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class RestaurantToppings
    {
        public int RestaurantId { get; set; }
        public int ToppingId { get; set; }
        public decimal? Price { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Toppings Topping { get; set; }
    }
}
