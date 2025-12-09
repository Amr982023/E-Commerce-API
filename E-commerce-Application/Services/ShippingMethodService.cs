using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ShippingMethodDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly IUnitOfWork _uow;

        public ShippingMethodService(IUnitOfWork uow)
        {
            _uow = uow;
        }     

        // Get all shipping methods
        public async Task<IEnumerable<ShippingMethodDto>> GetAllAsync()
        {
            var methods = await _uow.ShippingMethods.GetAllAsync();    
             return methods.Adapt<IEnumerable<ShippingMethodDto>>();
        }

        // Get available methods only
        public async Task<IEnumerable<ShippingMethodDto>> GetAvailableAsync()
        {
            var methods = await _uow.ShippingMethods.GetAvailableMethodsAsync();
            return methods.Adapt<IEnumerable<ShippingMethodDto>>();
        }

        // Get cheapest method
        public async Task<ShippingMethodDto?> GetCheapestAsync()
        {
            var method = await _uow.ShippingMethods.GetCheapestAsync();
            return method == null ? null : method.Adapt<ShippingMethodDto>();
        }

        // Get most expensive method
        public async Task<ShippingMethodDto?> GetMostExpensiveAsync()
        {
            var method = await _uow.ShippingMethods.GetMostExpensiveAsync();
            return method == null ? null : method.Adapt<ShippingMethodDto>();
        }
    }

}
