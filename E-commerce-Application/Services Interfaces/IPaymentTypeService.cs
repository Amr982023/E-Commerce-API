using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.PaymentTypeDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IPaymentTypeService
    {
        Task<IEnumerable<PaymentTypeDto>> GetAllAsync();
        Task<PaymentTypeDto?> GetByIdAsync(int id);
        Task<PaymentTypeDto?> GetByNameAsync(string name);
    }

}
