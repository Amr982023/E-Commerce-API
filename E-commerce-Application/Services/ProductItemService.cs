using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.DTOS;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class ProductItemService : IProductItemService
    {
        private readonly IUnitOfWork _uow;

        public ProductItemService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get by id with details
        public async Task<ProductItemDetailsDto?> GetByIdWithDetailsAsync(int id)
        {
            var item = await _uow.ProductItems.GetByIdWithDetailsAsync(id);
            if (item == null)
                return null;
  
            var images = await _uow.ProductItems.GetImagesAsync(id);

            var dto = item.Adapt<ProductItemDetailsDto>();
            dto.ProductName = item.Product.Name; 
            dto.Images = images;

            return dto;
        }

        // Get all SKUs for product
        public async Task<IEnumerable<ProductItemDto>> GetByProductIdAsync(int productId)
        {
            var items = await _uow.ProductItems.GetByProductIdAsync(productId);
            return items.Adapt<IEnumerable<ProductItemDto>>();
        }

        // ----------------------------------------
        // Get specific SKU based on selected options
        // e.g: productId + [colorId, sizeId] -> one ProductItem
        public async Task<IEnumerable<ProductItemDto>?> GetByOptionsAsync(int productId, IEnumerable<int> optionIds)
        {
            var items = await _uow.ProductItems.GetByOptionsAsync(productId, optionIds);
            if (items == null)
                return null;

            foreach (var item in items)
            {
                var images = await _uow.ProductItems.GetImagesAsync(item.Id);   
            }

            var dto = items.Adapt<IEnumerable<ProductItemDto>?>();
            
            return dto ;
        }

        // Stock checks
        public Task<bool> IsInStockAsync(int productItemId, int qty)
        {
            return _uow.ProductItems.IsInStockAsync(productItemId, qty);
        }

        public async Task<bool> DecreaseStockAsync(int productItemId, int qty)
        {
            if (!await _uow.ProductItems.IsInStockAsync(productItemId, qty))
                return false;

            await _uow.ProductItems.DecreaseStockAsync(productItemId, qty);
            await _uow.CompleteAsync();

            return true;
        }

        public async Task<bool> IncreaseStockAsync(int productItemId, int qty)
        {
            await _uow.ProductItems.IncreaseStockAsync(productItemId, qty);
            await _uow.CompleteAsync();
            return true;
        }

        // Price
        public Task<decimal> GetCurrentPriceAsync(int productItemId)
        {
            return _uow.ProductItems.GetCurrentPriceAsync(productItemId);
        }

        // Images
        public Task<IEnumerable<string>> GetImagesAsync(int productItemId)
        {
            return _uow.ProductItems.GetImagesAsync(productItemId);
        }

        // Validate Relationship
        public Task<bool> BelongsToProductAsync(int productItemId, int productId)
        {
            return _uow.ProductItems.BelongsToProductAsync(productItemId, productId);
        }

        // Available colors
        public async Task<IEnumerable<VariationOptionsDto>> GetAvailableColorsAsync(int productId)
        {
            var options = await _uow.ProductItems.GetAvailableColorsAsync(productId);
            return options.Adapt<IEnumerable<VariationOptionsDto>>();
        }

        // Stock summary
        public Task<ProductItemStockDto> GetStockSummaryAsync(int productId)
        {
            return _uow.ProductItems.GetStockSummaryAsync(productId);
        }

        // Bulk add product items
        public async Task<bool> AddProductItemsAsync(IEnumerable<CreateProductItemDto> items)
        {
            var entities = items.Adapt<IEnumerable<ProductItem>>();

            await _uow.ProductItems.AddProductItemsAsync(entities);
            await _uow.CompleteAsync();

            return true;
        }

    }

}
