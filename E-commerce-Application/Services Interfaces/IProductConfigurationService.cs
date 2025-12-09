using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductConfigurationDTOs;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.DTOs.VariationOptionDTOs;


namespace E_commerce_Application.Services_Interfaces
{
    public interface IProductConfigurationService
    {
        Task<IEnumerable<ProductConfigurationDto>> GetByProductItemAsync(int productItemId);

        Task<IEnumerable<VariationOptionDto>> GetOptionsForProductAsyncService(int productId);

        Task<bool> ExistsAsync(int productItemId, int variationOptionId);

        Task<IEnumerable<ProductItemDto>> GetProductItemsByOptionAsync(int variationOptionId);

        Task<ProductItemDto?> GetProductItemByOptionsAsync(int productId, IEnumerable<int> optionIds);

        Task<bool> AddConfigurationsAsync(int productItemId, IEnumerable<int> optionIds);

        Task<bool> RemoveConfigurationsAsync(int productItemId);
    }

}
