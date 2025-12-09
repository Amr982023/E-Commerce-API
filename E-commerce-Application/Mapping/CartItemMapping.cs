using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.CartItemDTOs;
using E_commerce_Application.DTOs.ReviewDTOs;
using E_commerce_Core.Models;

namespace E_commerce_Application.Mapping
{
    internal static class CartItemMapping
    {
        public static CartItemDto CartItemMapToDto(ShoppingCartItem item)
        {
            var price = item.ProductItem?.Price ?? 0m;

            return new CartItemDto
            {
                ProductItemId = item.ProductItemId,
                SKU = item.ProductItem?.SKU,
                ProductName = item.ProductItem?.Product?.Name,
                ProductImage = item.ProductItem?.ProductImage,
                Qty = item.Qty,
                UnitPrice = price
            };
        }
      
    }
}
