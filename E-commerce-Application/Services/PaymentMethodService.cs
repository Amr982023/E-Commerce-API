using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.PaymentMethodDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _uow;

        public PaymentMethodService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get methods
        public async Task<IEnumerable<PaymentMethodDto>> GetByAccountAsync(int accountId)
        {
            var methods = await _uow.PaymentMethods.GetPaymentMethodsByAccountAsync(accountId);
            return methods.Adapt<IEnumerable<PaymentMethodDto>>();
        }

        public async Task<IEnumerable<PaymentMethodDto>> GetValidByAccountAsync(int accountId)
        {
            var methods = await _uow.PaymentMethods.GetValidPaymentMethodsAsync(accountId);
            return methods.Adapt<IEnumerable<PaymentMethodDto>>();
        }

        public async Task<PaymentMethodDto?> GetDefaultAsync(int accountId)
        {
            var method = await _uow.PaymentMethods.GetDefaultPaymentMethodAsync(accountId);
            return method == null ? null : method.Adapt<PaymentMethodDto>();
        }

        public async Task<IEnumerable<PaymentMethodDto>> GetByProviderAsync(string provider)
        {
            var methods = await _uow.PaymentMethods.GetByProviderAsync(provider);
            return methods.Adapt<IEnumerable<PaymentMethodDto>>();
        }

        public async Task<PaymentMethodDto?> GetByIdWithDetailsAsync(int id)
        {
            var method = await _uow.PaymentMethods.GetByPaymentMethodWithDetailsAsync(id);
            return method == null ? null : method.Adapt<PaymentMethodDto>();
        }

        // Create
        public async Task<PaymentMethodDto> AddAsync(CreatePaymentMethodDto dto)
        {
            var method = new PaymentMethod
            {
                AccountId = dto.AccountId,
                PaymentTypeId = dto.PaymentTypeId,
                AccountNumber = dto.AccountNumber,
                Provider = dto.Provider,
                ExpiryDate = dto.ExpiryDate,
                IsDefault = dto.MakeDefault
            };

            await _uow.PaymentMethods.AddAsync(method);
            await _uow.CompleteAsync();

            if (dto.MakeDefault)
            {
                await _uow.PaymentMethods.SetDefaultPaymentMethodAsync(dto.AccountId, method.Id);
                await _uow.CompleteAsync();
            }

            return method.Adapt<PaymentMethodDto>();
        }

        // Update
        public async Task<bool> UpdateAsync(int id, UpdatePaymentMethodDto dto)
        {
            var method = await _uow.PaymentMethods.GetByIdAsync(id);
            if (method == null)
                return false;

            if (dto.PaymentTypeId.HasValue)
                method.PaymentTypeId = dto.PaymentTypeId.Value;

            if (!string.IsNullOrEmpty(dto.Provider))
                method.Provider = dto.Provider;

            if (dto.ExpiryDate.HasValue)
                method.ExpiryDate = dto.ExpiryDate.Value;

            _uow.PaymentMethods.Update(method);
            await _uow.CompleteAsync();
            return true;
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var method = await _uow.PaymentMethods.GetByIdAsync(id);
            if (method == null)
                return false;

            _uow.PaymentMethods.Delete(method);
            await _uow.CompleteAsync();
            return true;
        }

        // Business rules
        public async Task<bool> SetDefaultAsync(int accountId, int paymentMethodId)
        {
            var belongs = await _uow.PaymentMethods
                .PaymentMethodBelongsToAccountAsync(accountId, paymentMethodId);

            if (!belongs)
                return false;

            await _uow.PaymentMethods.SetDefaultPaymentMethodAsync(accountId, paymentMethodId);
            await _uow.CompleteAsync();
            return true;
        }

        public Task<bool> BelongsToAccountAsync(int accountId, int paymentMethodId)
        {
            return _uow.PaymentMethods.PaymentMethodBelongsToAccountAsync(accountId, paymentMethodId);
        }

    }


}
