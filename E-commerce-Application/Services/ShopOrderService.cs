using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.OrderLineDTOs;
using E_commerce_Application.DTOs.ShopOrderDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Consts;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class ShopOrderService : IShopOrderService
    {
        private readonly IUnitOfWork _uow;

        public ShopOrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

      

        // ------------ Get one order with all details -------------
        public async Task<ShopOrderDetailsDto?> GetOrderWithDetailsAsync(int orderId)
        {
            var order = await _uow.ShopOrders.GetOrderWithDetailsAsync(orderId);
            if (order == null)
                return null;

            var dto = new ShopOrderDetailsDto
            {
                Id = order.Id,
                UserId = order.UserId,
                UserFullName = order.User != null
                    ? $"{order.User.FirstName} {order.User.LastName}"
                    : null,

                OrderStatusId = order.OrderStatusId,
                StatusName = order.Status?.Status,

                OrderTotalCost = order.OrderTotalCost,
                OrderDate = order.OrderDate,

                PaymentMethodId = order.PaymentMethodId,
                PaymentMethodName = order.PaymentMethod?.PaymentType.Type,

                ShippingMethodId = order.ShippingMethodId,
                ShippingMethodName = order.ShippingMethod?.Name,

                ShippingAddressId = order.ShippingAddressId,
                ShippingAddressSummary = order.ShippingAddress != null
                    ? $"{order.ShippingAddress.Street}, {order.ShippingAddress.City}"
                    : null,
                Lines = order.OrderLines.Adapt<IEnumerable<OrderLineDto>>()
            };
                     
            return dto;
        }

        // ------------ Orders for account/user -------------
        public async Task<IEnumerable<ShopOrderSummaryDto>> GetOrdersForAccountAsync(int accountId)
        {
            
            var orders = await _uow.ShopOrders.GetOrdersForAccountAsync(accountId);
            return orders.Adapt<IEnumerable<ShopOrderSummaryDto>>();
        }

        public async Task<IEnumerable<ShopOrderSummaryDto>> GetOrdersForAccountPagedAsync(int accountId, int page, int size)
        {
            var orders = await _uow.ShopOrders.GetOrdersForAccountPagedAsync(accountId, page, size);
            return orders.Adapt<IEnumerable<ShopOrderSummaryDto>>();
        }

        public async Task<ShopOrderSummaryDto?> GetLastOrderAsync(int accountId)
        {
            var order = await _uow.ShopOrders.GetLastOrderAsync(accountId);
            return order == null ? null : order.Adapt<ShopOrderSummaryDto>();
        }

        // ------------ Status filters -------------
        public async Task<IEnumerable<ShopOrderSummaryDto>> GetOrdersByStatusAsync(enOrderStatus status)
        {
            var orders = await _uow.ShopOrders.GetOrdersByStatusAsync(status);
            return orders.Adapt<IEnumerable<ShopOrderSummaryDto>>();
        }

        public async Task<IEnumerable<ShopOrderSummaryDto>> GetPendingOrdersAsync()
        {
            var orders = await _uow.ShopOrders.GetPendingOrdersAsync();
            return orders.Adapt<IEnumerable<ShopOrderSummaryDto>>();
        }

        public async Task<IEnumerable<ShopOrderSummaryDto>> GetOrdersInRangeAsync(DateTime start, DateTime end)
        {
            var orders = await _uow.ShopOrders.GetOrdersInRangeAsync(start, end);
            return orders.Adapt<IEnumerable<ShopOrderSummaryDto>>();
        }

        // ------------ Commands (Cancel / Confirm / Set methods) -------------
        public async Task CancelOrderAsync(int orderId)
        {           
            await _uow.ShopOrders.CancelOrderAsync(orderId);
            await _uow.CompleteAsync();
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            await _uow.ShopOrders.ConfirmOrderAsync(orderId);
            await _uow.CompleteAsync();
        }

        public async Task SetShippingMethodAsync(int orderId, int shippingMethodId)
        {
            await _uow.ShopOrders.SetShippingMethodAsync(orderId, shippingMethodId);
            await _uow.CompleteAsync();
        }

        public async Task SetPaymentMethodAsync(int orderId, int paymentMethodId)
        {
            await _uow.ShopOrders.SetPaymentMethodAsync(orderId, paymentMethodId);
            await _uow.CompleteAsync();
        }

        // ------------ Reports / KPIs -------------
        public Task<decimal> GetTodaySalesAsync() => _uow.ShopOrders.GetTodaySalesAsync();

        public Task<decimal> GetAverageOrderValueAsync() => _uow.ShopOrders.GetAverageOrderValueAsync();
    }

}
