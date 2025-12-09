using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.PaymentTypeDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using Mapster;

namespace E_commerce_Application.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IUnitOfWork _uow;

        public PaymentTypeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get all payment types
        public async Task<IEnumerable<PaymentTypeDto>> GetAllAsync()
        {
            var types = await _uow.PaymentTypes.GetAllPaymentTypesAsync();
            return types.Adapt<IEnumerable<PaymentTypeDto>>();
        }

        // Get by id
        public async Task<PaymentTypeDto?> GetByIdAsync(int id)
        {
            var type = await _uow.PaymentTypes.GetByIdAsync(id);

            if (type == null)
                return null;

            return type.Adapt<PaymentTypeDto>();
        }

        // Get by name
        public async Task<PaymentTypeDto?> GetByNameAsync(string name)
        {
            var type = await _uow.PaymentTypes.GetByNameAsync(name);

            if (type == null)
                return null;

            return type.Adapt<PaymentTypeDto>();
        }
    }

}
