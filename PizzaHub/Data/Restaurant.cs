using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            CustomerOrderPizzas = new HashSet<CustomerOrderPizzas>();
            Pizza = new HashSet<Pizza>();
            RestaurantToppings = new HashSet<RestaurantToppings>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<CustomerOrderPizzas> CustomerOrderPizzas { get; set; }
        public virtual ICollection<Pizza> Pizza { get; set; }
        public virtual ICollection<RestaurantToppings> RestaurantToppings { get; set; }
    }
}
