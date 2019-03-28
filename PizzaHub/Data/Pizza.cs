using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class Pizza
    {
        public int RestaurantId { get; set; }
        public int SizeId { get; set; }
        public int StyleId { get; set; }
        public decimal? Price { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Sizes Size { get; set; }
        public virtual Styles Style { get; set; }
    }
}
