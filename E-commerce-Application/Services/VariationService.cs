using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.VariationDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.DTOS;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using Mapster;

namespace E_commerce_Application.Services
{
    public class VariationService : IVariationService
    {
        private readonly IUnitOfWork _uow;

        public VariationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get basic variation by Id
        public async Task<VariationDto?> GetByIdAsync(int id)
        {
            var variation = await _uow.Variations.GetByIdAsync(id);
            return variation?.Adapt<VariationDto>();
        }

        // Get variation + options
        public async Task<VariationWithOptionsDto?> GetWithOptionsAsync(int variationId)
        {
            var variation = await _uow.Variations.GetWithOptionsAsync(variationId);
            if (variation == null)
                return null;

            return variation.Adapt<VariationWithOptionsDto>();
        }

        // Variations available for specific ProductItem
        public async Task<IEnumerable<VariationDto>> GetVariationsForProductItemAsync(int productItemId)
        {
            var variations = await _uow.Variations.GetVariationsForProductAsync(productItemId);
            return variations.Adapt<IEnumerable<VariationDto>>();
        }

        // Options for variation for specific product
        public async Task<IEnumerable<VariationOptionsDto>> GetOptionsForProductVariationAsync(int productId, int variationId)
        {
            var options = await _uow.Variations.GetOptionsForProductVariationAsync(productId, variationId);

            return options.Adapt<IEnumerable<VariationOptionsDto>>();
        }

        // Check if variation is used (linked to products/items)
        public Task<bool> IsVariationUsedAsync(int variationId) => _uow.Variations.IsVariationUsedAsync(variationId);

        // Rename variation
        public async Task<bool> RenameVariationAsync(int variationId, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                return false;

            await _uow.Variations.RenameVariationAsync(variationId, newName);
            await _uow.CompleteAsync();
            return true;
        }

        // Add new option to variation
        public async Task<bool> AddOptionAsync(int variationId, string optionValue)
        {
            if (string.IsNullOrWhiteSpace(optionValue))
                return false;

            await _uow.Variations.AddOptionAsync(variationId, optionValue);
            await _uow.CompleteAsync();
            return true;
        }

        // Remove option
        public async Task<bool> RemoveOptionAsync(int variationOptionId)
        {
            await _uow.Variations.RemoveOptionAsync(variationOptionId);
            await _uow.CompleteAsync();
            return true;
        }

        // Variation tree for product
        // (e.g. Color -> [Red, Blue], Size -> [M, L, XL])
        public Task<IEnumerable<VariationWithOptionsDto>> GetVariationTreeForProductAsync(int productId)
        {
            return _uow.Variations.GetVariationTreeForProductAsync(productId);
        }
    }

}
