using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ShoppingCartDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IShoppingCartService
    {
        Task<CartDto> GetCartAsync(int accountId);
        Task AddItemAsync(int accountId, int productItemId, int qty);
        Task UpdateItemQuantityAsync(int accountId, int productItemId, int qty);
        Task RemoveItemAsync(int accountId, int productItemId);
        Task ClearCartAsync(int accountId);
    }

}
