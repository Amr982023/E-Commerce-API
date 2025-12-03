using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<ShopOrder> ShopOrders { get; set; }
    }

}
