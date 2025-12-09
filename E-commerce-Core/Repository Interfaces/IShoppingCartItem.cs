using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IShoppingCartItem :IGenericRepository<ShoppingCartItem>
    {
        Task UpdateItemQuantityAsync(int accountId, int productItemId, int qty);
        Task<IEnumerable<ShoppingCartItem>> GetItemsAsync(int accountId);
        Task<ShoppingCartItem?> GetCartItemAsync(int accountId, int productItemId);
        Task<decimal> GetItemTotalPriceAsync(int accountId, int productItemId);
        Task<ShoppingCartItem?> GetItemWithDetailsAsync(int accountId, int productItemId);
        Task<IEnumerable<ShoppingCartItem>> GetItemsWithDetailsAsync(int accountId);

    }
}
