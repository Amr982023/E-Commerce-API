using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IShoppingCart : IGenericRepository<ShoppingCart>
    {
        Task<ShoppingCart?> GetCartWithItemsAsync(int accountId);
        Task ClearCartAsync(int accountId);
        Task AddItemAsync(int ShoppingCartId, int productItemId, int qty);
        Task UpdateItemQuantityAsync(int accountId, int productItemId, int qty);
        Task RemoveItemAsync(int accountId, int productItemId);
        Task<int> GetTotalQuantityAsync(int accountId);
        Task<decimal> GetCartSubtotalAsync(int accountId);

    }
}
