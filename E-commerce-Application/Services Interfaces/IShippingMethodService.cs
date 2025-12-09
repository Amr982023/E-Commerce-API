using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ShippingMethodDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IShippingMethodService
    {
        Task<IEnumerable<ShippingMethodDto>> GetAllAsync();
        Task<IEnumerable<ShippingMethodDto>> GetAvailableAsync();
        Task<ShippingMethodDto?> GetCheapestAsync();
        Task<ShippingMethodDto?> GetMostExpensiveAsync();
    }

}
