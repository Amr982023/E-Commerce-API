using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.CartItemDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IShoppingCartItemService
    {
        Task<IEnumerable<CartItemDto>> GetItemsAsync(int accountId);
        Task<CartItemDto?> GetItemAsync(int accountId, int productItemId);
        Task<ShoppingCartItemWithDetailsDto?> GetItemWithDetailsAsync(int accountId, int productItemId);
        Task<IEnumerable<ShoppingCartItemWithDetailsDto>> GetItemsWithDetailsAsync(int accountId);

        Task UpdateItemQuantityAsync(int accountId, int productItemId, int qty);
        Task<decimal> GetItemTotalPriceAsync(int accountId, int productItemId);
    }
}
