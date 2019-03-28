using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class Sizes
    {
        public Sizes()
        {
            CustomerOrderPizzas = new HashSet<CustomerOrderPizzas>();
            Pizza = new HashSet<Pizza>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerOrderPizzas> CustomerOrderPizzas { get; set; }
        public virtual ICollection<Pizza> Pizza { get; set; }
    }
}
