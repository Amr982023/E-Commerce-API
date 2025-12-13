using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductItemDTOs;

namespace E_commerce_Application.DTOs.CartItemDTOs
{
    public class ShoppingCartItemWithDetailsDto
    {
        public int Id { get; set; }

        public int ShoppingCartId { get; set; }

        public int ProductItemId { get; set; }

        public int Qty { get; set; }

        // -------- Product Item Details --------
        public ProductItemDto? ProductItem { get; set; }

        // -------- Calculated --------
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => UnitPrice * Qty;
    }

}
