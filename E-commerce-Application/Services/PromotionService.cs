using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.PromotionDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _uow;

        public PromotionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get active promotions
        public async Task<IEnumerable<PromotionDto>> GetActiveAsync()
        {
            var now = DateTime.UtcNow;

            var promotions = await _uow.Promotions.FindAllAsync(
                p => p.StartDate <= now && p.EndDate >= now
            );

            return promotions.Adapt<IEnumerable<PromotionDto>>();
        }

        // Get by category
        public async Task<IEnumerable<PromotionDto>> GetForCategoryAsync(int categoryId)
        {
            var promotions = await _uow.Promotions.GetPromotionsForCategoryAsync(categoryId);
            return promotions.Adapt<IEnumerable<PromotionDto>>();
        }

        // Get by product
        public async Task<IEnumerable<PromotionDto>> GetForProductAsync(int productId)
        {
            var promotions = await _uow.Promotions.GetPromotionsForProductAsync(productId);
            return promotions.Adapt<IEnumerable<PromotionDto>>();
        }

        // Get by product item (SKU)
        public async Task<IEnumerable<PromotionDto>> GetForProductItemAsync(int productItemId)
        {
            var promotions = await _uow.Promotions.GetPromotionsForProductItemAsync(productItemId);
            return promotions.Adapt<IEnumerable<PromotionDto>>();
        }

        // Best discount rate for ProductItem
        public async Task<decimal> GetBestDiscountRateForProductItemAsync(int productItemId)
        {
            var now = DateTime.UtcNow;

            var promotions = await _uow.Promotions.GetPromotionsForProductItemAsync(productItemId);

            var activePromos = promotions
                .Where(p => p.StartDate <= now && p.EndDate >= now)
                .ToList();

            if (!activePromos.Any())
                return 0m;

            return activePromos.Max(p => p.DiscountRate);
        }

        // Apply best discount to price
        public async Task<ApplyPromotionResultDto> ApplyBestDiscountAsync(int productItemId)
        {
            var bestRate = await GetBestDiscountRateForProductItemAsync(productItemId);
            var basePrice = await _uow.ProductItems.GetCurrentPriceAsync(productItemId);

            var discountAmount = Math.Round(basePrice * bestRate, 2);
            var finalPrice = basePrice - discountAmount;

            return new ApplyPromotionResultDto
            {
                OriginalPrice = basePrice,
                DiscountRate = bestRate,
                DiscountAmount = discountAmount,
                FinalPrice = finalPrice
            };
        }

        // Create
        public async Task<PromotionDto> CreateAsync(PromotionDto dto)
        {
            var entity = dto.Adapt<Promotion>();

            await _uow.Promotions.AddAsync(entity);
            await _uow.CompleteAsync();

            return entity.Adapt<PromotionDto>();
        }

        // Update
        public async Task<bool> UpdateAsync(int id, PromotionDto dto)
        {
            var entity = await _uow.Promotions.GetByIdAsync(id);
            if (entity == null)
                return false;

            if (dto.Name!=null) entity.Name = dto.Name;
            if (dto.DiscountRate != 0m) entity.DiscountRate = dto.DiscountRate;
            if (dto.Description != null) entity.Description = dto.Description;
            if (dto.StartDate != DateTime.MinValue) entity.StartDate = dto.StartDate;
            if (dto.EndDate != DateTime.MinValue) entity.EndDate = dto.EndDate;

            _uow.Promotions.Update(entity);
            await _uow.CompleteAsync();

            return true;
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Promotions.GetByIdAsync(id);
            if (entity == null)
                return false;

            _uow.Promotions.Delete(entity);
            await _uow.CompleteAsync();

            return true;
        }

        // Set categories for a promotion
        // (using PromotionCategory repo)
        public async Task<bool> SetCategoriesForPromotionAsync(int promotionId, IEnumerable<int> categoryIds)
        {
            // Delete old
            await _uow.PromotionCategories.RemoveAllCategoriesForPromotionAsync(promotionId);

            // Add new
            await _uow.PromotionCategories.AddCategoriesToPromotionAsync(promotionId, categoryIds);

            await _uow.CompleteAsync();
            return true;
        }

    }

}
