using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IPaymentMethod : IGenericRepository<PaymentMethod>
    {
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsByAccountAsync(int accountId);

        Task<PaymentMethod> GetDefaultPaymentMethodAsync(int accountId);

        Task SetDefaultPaymentMethodAsync(int accountId, int paymentMethodId);

        Task<bool> PaymentMethodBelongsToAccountAsync(int accountId, int paymentMethodId);

        bool IsExpired(PaymentMethod method);

        Task<IEnumerable<PaymentMethod>> GetValidPaymentMethodsAsync(int accountId);

        Task<IEnumerable<PaymentMethod>> GetByProviderAsync(string provider);

        Task<PaymentMethod> GetByPaymentMethodWithDetailsAsync(int id);

        string MaskCardNumber(string number);

    }
}
