using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.AddressDTOS;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAddressesByAccountAsync(int accountId);
        Task<AddressDto?> GetAddressByIdAsync(int id);
        Task<AddressDto> CreateAddressAsync(CreateAddressDto dto);
        Task<bool> UpdateAddressAsync(int id, UpdateAddressDto dto);
        Task<bool> DeleteAddressAsync(int id);

        Task<bool> SetDefaultAddressAsync(int accountId, int addressId);
        Task<AddressDto?> GetDefaultAddressAsync(int accountId);
    }
}
