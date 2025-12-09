using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.CountryDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using Mapster;

namespace E_commerce_Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _uow;

        public CountryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            var countries = await _uow.Countries.GetAllCountriesAsync();
            return countries.Adapt<IEnumerable<CountryDto>>();
        }

        public async Task<CountryDto?> GetByIdAsync(int id)
        {
            var country = await _uow.Countries.GetCountryByIdAsync(id);
            return country?.Adapt<CountryDto>();
        }

        public async Task<CountryDto?> GetByNameAsync(string name)
        {
            var country = await _uow.Countries.GetCountryByNameAsync(name);
            return country?.Adapt<CountryDto>();
        }

        public async Task<CountryDto?> GetByCodeAsync(string code)
        {
            var country = await _uow.Countries.GetCountryByCodeAsync(code);
            return country?.Adapt<CountryDto>();
        }
    }
}
