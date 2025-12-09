using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.CountryDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task<CountryDto?> GetByIdAsync(int id);
        Task<CountryDto?> GetByNameAsync(string name);
        Task<CountryDto?> GetByCodeAsync(string code);
    }

}
