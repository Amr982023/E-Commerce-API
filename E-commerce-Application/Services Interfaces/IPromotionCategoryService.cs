using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IPromotionCategoryService
    {
        Task AddCategoryToPromotionAsync(int promotionId, int categoryId);
        Task AddCategoriesToPromotionAsync(int promotionId, IEnumerable<int> categoryIds);
        Task RemoveCategoryFromPromotionAsync(int promotionId, int categoryId);
        Task RemoveAllCategoriesForPromotionAsync(int promotionId);
        Task ReplaceCategoriesForPromotionAsync(int promotionId, IEnumerable<int> categoryIds);
    }

}
