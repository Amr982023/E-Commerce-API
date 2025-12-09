using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.Dtos.OrderLineDTOs
{
    public class CreateOrderLineDto
    {
        public int ShopOrderId { get; set; }
        public int ProductItemId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }

}
