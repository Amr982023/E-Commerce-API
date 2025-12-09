using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.VariationOptionDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class VariationOptionService : IVariationOptionService
    {
        private readonly IUnitOfWork _uow;

        public VariationOptionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get options for a specific variation
        public async Task<IEnumerable<VariationOptionDto>> GetOptionsForVariationAsync(int variationId)
        {
            var options = await _uow.VariationOptions.GetOptionsForVariationAsync(variationId);
            return options.Adapt<IEnumerable<VariationOptionDto>>();
        }

        // Check option existence (prevent duplicates)
        public async Task<bool> OptionExistsAsync(int variationId, string value)
        {
            return await _uow.VariationOptions.OptionExistsAsync(variationId, value);
        }

        // Get options for a variation but filtered by product
        // (e.g. all colors for this product & this variation)
        public async Task<IEnumerable<VariationOptionDto>> GetOptionsForProductAsync(int productId, int variationId)
        {
            var options = await _uow.VariationOptions.GetOptionsForProductAsync(productId, variationId);
            return options.Adapt<IEnumerable<VariationOptionDto>>();
        }

        // Get options that belong to a specific productItem
        // (all options that define this SKU)
        public async Task<IEnumerable<VariationOptionDto>> GetOptionsForProductItemAsync(int productItemId)
        {
            var options = await _uow.VariationOptions.GetOptionsForProductItemAsync(productItemId);
            return options.Adapt<IEnumerable<VariationOptionDto>>();
        }

        // Check if option is used in any product configuration
        // (useful before delete)
        public Task<bool> IsOptionUsedAsync(int optionId)
        {
            return _uow.VariationOptions.IsOptionUsedAsync(optionId);
        }

        // Directly return DTOs from repo
        public async Task<IEnumerable<VariationOptionDto>> GetOptionsDtoForVariationAsync(int variationId)
        {
            var options = await _uow.VariationOptions.GetOptionsDtoForVariationAsync(variationId);
            return options.Adapt<IEnumerable<VariationOptionDto>>();
        }
    }

}
