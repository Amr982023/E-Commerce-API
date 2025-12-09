using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IPromotionCategory : IGenericRepository<PromotionCategory>
    {
        Task AddCategoryToPromotionAsync(int promotionId, int categoryId);

        Task AddCategoriesToPromotionAsync(int promotionId, IEnumerable<int> categoryIds);

        Task RemoveCategoryFromPromotionAsync(int promotionId, int categoryId);

        Task RemoveAllCategoriesForPromotionAsync(int promotionId);

    }
}
