using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IOrderLine : IGenericRepository<OrderLine>
    {
        Task<IEnumerable<OrderLine>> GetOrderLinesByOrderIdAsync(int orderId);

        Task<OrderLine> GetOrderLineAsync(int orderId, int productItemId);

        Task<OrderLine> GetOrderLineWithDetailsAsync(int orderId, int productItemId);
        
        decimal CalculateLineTotal(OrderLine line);

    }
}
