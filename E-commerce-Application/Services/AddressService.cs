using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.AddressDTOS;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _uow;

        public AddressService(IUnitOfWork uow)
        {
            _uow = uow;
        }

   
        // Get addresses for a specific account
        public async Task<IEnumerable<AddressDto>> GetAddressesByAccountAsync(int accountId)
        {
            var addresses = await _uow.Addresses.GetAddressesByAccountIdAsync(accountId);
           
            return addresses.Adapt<List<AddressDto>>();             
        }

        // Get single address by Id (with details)
        public async Task<AddressDto?> GetAddressByIdAsync(int id)
        {
            var address = await _uow.Addresses.GetAddressWithDetailsAsync(id);
            if (address == null)
                return null;

            var dto = address.Adapt<AddressDto>();
            return dto;
        }

        // Create new address
        public async Task<AddressDto> CreateAddressAsync(CreateAddressDto dto)
        {
            var address = new Address
            {                
                UnitNumber = dto.UnitNumber,
                Street = dto.Street,
                Region = dto.Region,
                City = dto.City,
                PostalCode = dto.PostalCode,
                CountryId = dto.CountryId,
                IsDefault = dto.MakeDefault
            };

            await _uow.Addresses.AddAsync(address);
            await _uow.CompleteAsync();

            // Remove Default From Any other Address
            if (dto.MakeDefault)
            {
                await _uow.Addresses.SetDefaultAddressAsync(dto.AccountId, address.Id);
                await _uow.CompleteAsync();
            }

            var result = address.Adapt<AddressDto>();
            return result;
        }

        // Update existing address
        public async Task<bool> UpdateAddressAsync(int id, UpdateAddressDto dto)
        {
            var address = await _uow.Addresses.GetByIdAsync(id);
            if (address == null)
                return false;

            if (dto.UnitNumber != null) address.UnitNumber = dto.UnitNumber;
            if (dto.Street != null) address.Street = dto.Street;
            if (dto.Region != null) address.Region = dto.Region;
            if (dto.City != null) address.City = dto.City;
            if (dto.PostalCode != null) address.PostalCode = dto.PostalCode;
            if (dto.CountryId.HasValue) address.CountryId = dto.CountryId.Value;

            _uow.Addresses.Update(address);
            await _uow.CompleteAsync();

            return true;
        }

        // Delete address
        public async Task<bool> DeleteAddressAsync(int id)
        {
            var address = await _uow.Addresses.GetByIdAsync(id);
            if (address == null)
                return false;

            _uow.Addresses.Delete(address);
            await _uow.CompleteAsync();
            return true;
        }

        // Set default address for an account
        public async Task<bool> SetDefaultAddressAsync(int accountId, int addressId)
        {
            await _uow.Addresses.SetDefaultAddressAsync(accountId, addressId);
            await _uow.CompleteAsync();
            return true;
        }

        // Get default address for an account
        public async Task<AddressDto?> GetDefaultAddressAsync(int accountId)
        {
           
            var defaultAddress = await _uow.Addresses.GetDefaultAddressAsync(accountId);
              
            if (defaultAddress == null)
                return null;

            var dto = defaultAddress.Adapt<AddressDto>();   
            return dto;
        }
    }

}
