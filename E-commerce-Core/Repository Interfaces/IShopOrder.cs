using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Consts;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IShopOrder : IGenericRepository<ShopOrder>
    {
        Task<ShopOrder> GetOrderWithDetailsAsync(int orderId);
        Task CancelOrderAsync(int orderId);
        Task<IEnumerable<ShopOrder>> GetOrdersForAccountAsync(int accountId);
        Task<IEnumerable<ShopOrder>> GetOrdersForAccountPagedAsync(int accountId, int page, int size);
        Task<ShopOrder?> GetLastOrderAsync(int accountId);
        Task ConfirmOrderAsync(int orderId);
        Task SetShippingMethodAsync(int orderId, int shippingMethodId);
        Task SetPaymentMethodAsync(int orderId, int paymentMethodId);
        Task<IEnumerable<ShopOrder>> GetOrdersByStatusAsync(enOrderStatus status);
        Task<IEnumerable<ShopOrder>> GetPendingOrdersAsync();
        Task<IEnumerable<ShopOrder>> GetOrdersInRangeAsync(DateTime start, DateTime end);
        Task<decimal> GetTodaySalesAsync();
        Task<decimal> GetAverageOrderValueAsync();

    }
}
