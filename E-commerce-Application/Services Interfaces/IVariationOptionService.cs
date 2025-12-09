using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application;
using E_commerce_Application.DTOs.VariationOptionDTOs;


namespace E_commerce_Application.Services_Interfaces
{
    public interface IVariationOptionService
    {
        Task<IEnumerable<VariationOptionDto>> GetOptionsForVariationAsync(int variationId);
        Task<bool> OptionExistsAsync(int variationId, string value);
        Task<IEnumerable<VariationOptionDto>> GetOptionsForProductAsync(int productId, int variationId);
        Task<IEnumerable<VariationOptionDto>> GetOptionsForProductItemAsync(int productItemId);
        Task<bool> IsOptionUsedAsync(int optionId);
        Task<IEnumerable<VariationOptionDto>> GetOptionsDtoForVariationAsync(int variationId);
    }

}
