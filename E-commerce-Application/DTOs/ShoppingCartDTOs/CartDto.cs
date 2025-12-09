using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.CartItemDTOs;

namespace E_commerce_Application.DTOs.ShoppingCartDTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int AccountId { get; set; }

        public IEnumerable<CartItemDto> Items { get; set; } = new List<CartItemDto>();

        public int TotalQuantity { get; set; }
        public decimal Subtotal { get; set; }
    }

}
