using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface ICountry
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();

        Task<Country> GetCountryByIdAsync(int id);

        Task<Country> GetCountryByNameAsync(string name);

        Task<Country> GetCountryByCodeAsync(string Code);
    }
}
