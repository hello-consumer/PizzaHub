using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class CustomerOrderPizzaToppings
    {
        public int CustomerOrderId { get; set; }
        public int LineItemId { get; set; }
        public int ToppingId { get; set; }

        public virtual CustomerOrderPizzas CustomerOrderPizzas { get; set; }
        public virtual Toppings Topping { get; set; }
    }
}
