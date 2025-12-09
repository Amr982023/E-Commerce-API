using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.DTOS;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IVariationOption : IGenericRepository<VariationOption>
    {
        Task<IEnumerable<VariationOption>> GetOptionsForVariationAsync(int variationId);
        Task<bool> OptionExistsAsync(int variationId, string value);
        Task<IEnumerable<VariationOption>> GetOptionsForProductAsync(int productId, int variationId);
        Task<IEnumerable<VariationOption>> GetOptionsForProductItemAsync(int productItemId);
        Task<bool> IsOptionUsedAsync(int optionId);
        Task<IEnumerable<VariationOptionDto>> GetOptionsDtoForVariationAsync(int variationId);
    }
}
