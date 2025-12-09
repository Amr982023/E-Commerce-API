using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IShippingMethod : IGenericRepository<ShippingMethod>
    {
        Task<ShippingMethod> GetCheapestAsync();
        Task<ShippingMethod> GetMostExpensiveAsync();
        Task<IEnumerable<ShippingMethod>> GetAvailableMethodsAsync();
    }
}
