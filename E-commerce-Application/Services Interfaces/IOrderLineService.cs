using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.OrderLineDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IOrderLineService
    {
        Task<IEnumerable<OrderLineDto>> GetByOrderIdAsync(int orderId);
        Task<OrderLineDto?> GetLineAsync(int orderId, int productItemId);
        Task<OrderLineDto> GetLineWithDetailsAsync(int orderId, int productItemId);

        Task<OrderLineDto> AddLineAsync(CreateOrderLineDto dto);
        Task<bool> UpdateQuantityAsync(int orderId, int productItemId, int qty);
        Task<bool> RemoveLineAsync(int orderId, int productItemId);

        decimal CalculateLineTotal(int qty, decimal price);
    }

}
