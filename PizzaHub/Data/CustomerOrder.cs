using System;
using System.Collections.Generic;

namespace PizzaHub.Data
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            CustomerOrderPizzas = new HashSet<CustomerOrderPizzas>();
        }

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryZipCode { get; set; }
        public string SpecialInstructions { get; set; }

        public virtual ICollection<CustomerOrderPizzas> CustomerOrderPizzas { get; set; }
    }
}
