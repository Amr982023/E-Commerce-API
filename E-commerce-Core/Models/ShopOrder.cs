using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class ShopOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotalCost { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int ShippingAddressId { get; set; }
        public Address ShippingAddress { get; set; }

        public int ShippingMethodId { get; set; }
        public ShippingMethod ShippingMethod { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }

}
