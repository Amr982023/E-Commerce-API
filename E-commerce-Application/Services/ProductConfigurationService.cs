using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductConfigurationDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using Mapster;
using E_commerce_Application.DTOs.VariationOptionDTOs;
using E_commerce_Application.DTOs.ProductItemDTOs;

namespace E_commerce_Application.Services
{
    public class ProductConfigurationService : IProductConfigurationService
    {
        private readonly IUnitOfWork _uow;

        public ProductConfigurationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get all configurations for a product item
        public async Task<IEnumerable<ProductConfigurationDto>> GetByProductItemAsync(int productItemId)
        {
            var configs = await _uow.ProductConfigurations.GetByProductItemAsync(productItemId);

            return configs.Select(pc => new ProductConfigurationDto
            {
                ProductItemId = pc.ProductItemId,
                VariationOptionId = pc.VariationOptionId,
                VariationName = pc.VariationOption?.Variation?.Name,
                OptionValue = pc.VariationOption?.Value
            });
        }

        // Get options for a product (for filters or selection UI)
        public Task<IEnumerable<VariationOptionDto>> GetOptionsForProductAsyncService(int productId)
        {
            var options =  _uow.ProductConfigurations.GetOptionsForProductAsync(productId);

            return options.Adapt<Task<IEnumerable<VariationOptionDto>>>();
        }

        // Check if configuration exists
        public Task<bool> ExistsAsync(int productItemId, int variationOptionId)
        {
            return _uow.ProductConfigurations.ExistsAsync(productItemId, variationOptionId);
        }

        // Get items that have a specific option
        public Task<IEnumerable<ProductItemDto>> GetProductItemsByOptionAsync(int variationOptionId)
        {
            var items =  _uow.ProductConfigurations.GetProductItemsByOptionAsync(variationOptionId);

            return items.Adapt<Task<IEnumerable<ProductItemDto>>>();
        }

        // Get product item matching all selected options
        // (e.g. color = red + size = XL)
        public async Task<ProductItemDto?> GetProductItemByOptionsAsync(int productId, IEnumerable<int> optionIds)
        {
            var item = await _uow.ProductConfigurations.GetProductItemByOptionsAsync(productId, optionIds);

            return item?.Adapt<ProductItemDto>();
        }

        // Add configurations to product item
        public async Task<bool> AddConfigurationsAsync(int productItemId, IEnumerable<int> optionIds)
        {
            await _uow.ProductConfigurations.AddConfigurationsAsync(productItemId, optionIds);
            await _uow.CompleteAsync();

            return true;
        }

        // Remove configurations for a product item
        public async Task<bool> RemoveConfigurationsAsync(int productItemId)
        {
            await _uow.ProductConfigurations.RemoveConfigurationsForProductItemAsync(productItemId);
            await _uow.CompleteAsync();

            return true;
        }
        
    }
}
