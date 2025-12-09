using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;

namespace E_commerce_Application.Services
{
    public class PromotionCategoryService : IPromotionCategoryService
    {
        private readonly IUnitOfWork _uow;

        public PromotionCategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Add single category to a promotion
        public async Task AddCategoryToPromotionAsync(int promotionId, int categoryId)
        {
            await _uow.PromotionCategories.AddCategoryToPromotionAsync(promotionId, categoryId);
            await _uow.CompleteAsync();
        }

        // Add multiple categories to a promotion
        public async Task AddCategoriesToPromotionAsync(int promotionId, IEnumerable<int> categoryIds)
        {           
            var distinctIds = categoryIds.Distinct().ToList();

            await _uow.PromotionCategories.AddCategoriesToPromotionAsync(promotionId, distinctIds);
            await _uow.CompleteAsync();
        }

        // Remove single category from a promotion
        public async Task RemoveCategoryFromPromotionAsync(int promotionId, int categoryId)
        {
            await _uow.PromotionCategories.RemoveCategoryFromPromotionAsync(promotionId, categoryId);
            await _uow.CompleteAsync();
        }

        // Remove all categories for a promotion
        public async Task RemoveAllCategoriesForPromotionAsync(int promotionId)
        {
            await _uow.PromotionCategories.RemoveAllCategoriesForPromotionAsync(promotionId);
            await _uow.CompleteAsync();
        }

        // Replace all categories (helper for admin UI) 
        public async Task ReplaceCategoriesForPromotionAsync(int promotionId, IEnumerable<int> categoryIds)
        {
            // Remove Old
            await _uow.PromotionCategories.RemoveAllCategoriesForPromotionAsync(promotionId);

            // Add New
            var distinctIds = categoryIds.Distinct().ToList();
            if (distinctIds.Any())
            {
                await _uow.PromotionCategories.AddCategoriesToPromotionAsync(promotionId, distinctIds);
            }

            await _uow.CompleteAsync();
        }

    }

}
