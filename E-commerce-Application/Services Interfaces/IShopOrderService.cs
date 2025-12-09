using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ShopOrderDTOs;
using E_commerce_Core.Consts;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IShopOrderService
    {
        Task<ShopOrderDetailsDto?> GetOrderWithDetailsAsync(int orderId);

        Task<IEnumerable<ShopOrderSummaryDto>> GetOrdersForAccountAsync(int accountId);
        Task<IEnumerable<ShopOrderSummaryDto>> GetOrdersForAccountPagedAsync(int accountId, int page, int size);
        Task<ShopOrderSummaryDto?> GetLastOrderAsync(int accountId);

        Task<IEnumerable<ShopOrderSummaryDto>> GetOrdersByStatusAsync(enOrderStatus status);
        Task<IEnumerable<ShopOrderSummaryDto>> GetPendingOrdersAsync();
        Task<IEnumerable<ShopOrderSummaryDto>> GetOrdersInRangeAsync(DateTime start, DateTime end);

        Task CancelOrderAsync(int orderId);
        Task ConfirmOrderAsync(int orderId);
        Task SetShippingMethodAsync(int orderId, int shippingMethodId);
        Task SetPaymentMethodAsync(int orderId, int paymentMethodId);

        Task<decimal> GetTodaySalesAsync();
        Task<decimal> GetAverageOrderValueAsync();
    }

}
