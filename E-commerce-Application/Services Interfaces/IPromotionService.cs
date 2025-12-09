using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.PromotionDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IPromotionService
    {
        // Basic reads
        Task<IEnumerable<PromotionDto>> GetActiveAsync();
        Task<IEnumerable<PromotionDto>> GetForCategoryAsync(int categoryId);
        Task<IEnumerable<PromotionDto>> GetForProductAsync(int productId);
        Task<IEnumerable<PromotionDto>> GetForProductItemAsync(int productItemId);

        // Discount calculations
        Task<decimal> GetBestDiscountRateForProductItemAsync(int productItemId);
        Task<ApplyPromotionResultDto> ApplyBestDiscountAsync(int productItemId, decimal basePrice);

        // CRUD 
        Task<PromotionDto> CreateAsync(PromotionDto dto);
        Task<bool> UpdateAsync(int id, PromotionDto dto);
        Task<bool> DeleteAsync(int id);

        // PromotionCategory
        Task<bool> SetCategoriesForPromotionAsync(int promotionId, IEnumerable<int> categoryIds);
    }

}
