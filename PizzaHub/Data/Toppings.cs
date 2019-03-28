using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class Toppings
    {
        public Toppings()
        {
            CustomerOrderPizzaToppings = new HashSet<CustomerOrderPizzaToppings>();
            RestaurantToppings = new HashSet<RestaurantToppings>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerOrderPizzaToppings> CustomerOrderPizzaToppings { get; set; }
        public virtual ICollection<RestaurantToppings> RestaurantToppings { get; set; }
    }
}
