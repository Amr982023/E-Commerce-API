using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.VariationDTOs;
using E_commerce_Core.DTOS;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IVariationService
    {
        Task<VariationDto?> GetByIdAsync(int id);
        Task<VariationWithOptionsDto?> GetWithOptionsAsync(int variationId);

        Task<IEnumerable<VariationDto>> GetVariationsForProductItemAsync(int productItemId);
        Task<IEnumerable<VariationOptionsDto>> GetOptionsForProductVariationAsync(int productId, int variationId);

        Task<bool> IsVariationUsedAsync(int variationId);

        Task<bool> RenameVariationAsync(int variationId, string newName);
        Task<bool> AddOptionAsync(int variationId, string optionValue);
        Task<bool> RemoveOptionAsync(int variationOptionId);

        Task<IEnumerable<VariationWithOptionsDto>> GetVariationTreeForProductAsync(int productId);
    }

}
