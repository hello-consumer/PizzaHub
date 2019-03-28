using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class CustomerOrderPizzas
    {
        public CustomerOrderPizzas()
        {
            CustomerOrderPizzaToppings = new HashSet<CustomerOrderPizzaToppings>();
        }

        public int CustomerOrderId { get; set; }
        public int LineItemId { get; set; }
        public int RestaurantId { get; set; }
        public int? StyleId { get; set; }
        public int? SizeId { get; set; }
        public decimal? OrderPrice { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual Sizes Size { get; set; }
        public virtual Styles Style { get; set; }
        public virtual ICollection<CustomerOrderPizzaToppings> CustomerOrderPizzaToppings { get; set; }
    }
}
