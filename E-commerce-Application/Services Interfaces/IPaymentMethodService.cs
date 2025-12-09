using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.PaymentMethodDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IPaymentMethodService
    {
        // Get
        Task<IEnumerable<PaymentMethodDto>> GetByAccountAsync(int accountId);
        Task<IEnumerable<PaymentMethodDto>> GetValidByAccountAsync(int accountId);
        Task<PaymentMethodDto?> GetDefaultAsync(int accountId);
        Task<IEnumerable<PaymentMethodDto>> GetByProviderAsync(string provider);
        Task<PaymentMethodDto?> GetByIdWithDetailsAsync(int id);

        // Create / Update / Delete
        Task<PaymentMethodDto> AddAsync(CreatePaymentMethodDto dto);
        Task<bool> UpdateAsync(int id, UpdatePaymentMethodDto dto);
        Task<bool> DeleteAsync(int id);

        // Business rules
        Task<bool> SetDefaultAsync(int accountId, int paymentMethodId);
        Task<bool> BelongsToAccountAsync(int accountId, int paymentMethodId);
    }


}
