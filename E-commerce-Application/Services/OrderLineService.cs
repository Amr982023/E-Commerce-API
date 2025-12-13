using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.OrderLineDTOs;
using E_commerce_Application.DTOs.OrderLineDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IUnitOfWork _uow;

        public OrderLineService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get all lines for an order
        public async Task<IEnumerable<OrderLineDto>> GetByOrderIdAsync(int orderId)
        {
            var lines = await _uow.OrderLines.GetOrderLinesByOrderIdAsync(orderId);

            return lines.Select(line =>
            {
                var dto = line.Adapt<OrderLineDto>();
                dto.LineTotal = _uow.OrderLines.CalculateLineTotal(line);
                return dto;
            });
        }

        // Get specific line (simple)
        public async Task<OrderLineDto?> GetLineAsync(int orderId, int productItemId)
        {
            var line = await _uow.OrderLines.GetOrderLineAsync(orderId, productItemId);
            if (line == null)
                return null;

            var dto = line.Adapt<OrderLineDto>();
            dto.LineTotal = _uow.OrderLines.CalculateLineTotal(line);
            return dto;
        }

        // Get line with details (includes)
        public async Task<OrderLineWithDetailsDto?> GetLineWithDetailsAsync(int orderId, int productItemId)
        {
            var line = await _uow.OrderLines.GetOrderLineWithDetailsAsync(orderId, productItemId);
            if (line == null)
                return null;

            var dto = line.Adapt<OrderLineWithDetailsDto>();
            dto.LineTotal = _uow.OrderLines.CalculateLineTotal(line);
            return dto;
        }

        // Add new order line
        public async Task<OrderLineDto> AddLineAsync(CreateOrderLineDto dto)
        {
            var line = new OrderLine
            {
                ShopOrderId = dto.ShopOrderId,
                ProductItemId = dto.ProductItemId,
                Qty = dto.Qty,
                Price = dto.Price
            };

            await _uow.OrderLines.AddAsync(line);
            await _uow.CompleteAsync();

            var result = line.Adapt<OrderLineDto>();
            result.LineTotal = _uow.OrderLines.CalculateLineTotal(line);
            return result;
        }

        // Update quantity for a given line
        public async Task<bool> UpdateQuantityAsync(int orderId, int productItemId, int qty)
        {
            var line = await _uow.OrderLines.GetOrderLineAsync(orderId, productItemId);
            if (line == null)
                return false;

            line.Qty = qty;

            _uow.OrderLines.Update(line);
            await _uow.CompleteAsync();

            return true;
        }

        // Remove line from an order
        public async Task<bool> RemoveLineAsync(int orderId, int productItemId)
        {
            var line = await _uow.OrderLines.GetOrderLineAsync(orderId, productItemId);
            if (line == null)
                return false;

            _uow.OrderLines.Delete(line);
            await _uow.CompleteAsync();

            return true;
        }

        // Line total calculation (pure)
        public decimal CalculateLineTotal(int qty, decimal price)
        { 
            return qty * price;
        }
    }

}
