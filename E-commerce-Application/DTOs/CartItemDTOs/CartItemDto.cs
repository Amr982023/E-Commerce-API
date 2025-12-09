using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.DTOs.CartItemDTOs
{
    public class CartItemDto
    {
        public int ProductItemId { get; set; }
        public string? SKU { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }

        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }     
        public decimal LineTotal => UnitPrice * Qty;
    }

}
