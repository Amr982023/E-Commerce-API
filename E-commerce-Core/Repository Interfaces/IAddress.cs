using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IAddress : IGenericRepository<Address>
    {
        Task<IEnumerable<Address>> GetAddressesByAccountIdAsync(int accountId);

        Task SetDefaultAddressAsync(int accountId, int addressId);

        Task<Address> GetDefaultAddressAsync(int accountId);

        Task<Address> GetAddressWithDetailsAsync(int id);
    }
}
