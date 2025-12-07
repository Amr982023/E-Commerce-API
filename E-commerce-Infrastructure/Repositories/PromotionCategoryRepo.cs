using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Models;
using E_commerce_Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Infrastructure.Repositories
{
    public class PromotionCategoryRepo :GenericRepository<PromotionCategory>,IPromotionCategory
    {
        public PromotionCategoryRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddCategoriesToPromotionAsync(int promotionId, IEnumerable<int> categoryIds)
        {
            await _context.PromotionCategories.AddRangeAsync(
                categoryIds.Select(categoryId => new PromotionCategory
                {
                    PromotionId = promotionId,
                    CategoryId = categoryId
                })
            );
        }

        public async Task AddCategoryToPromotionAsync(int promotionId, int categoryId)
        {
            await _context.PromotionCategories.AddAsync(new PromotionCategory
            {
                PromotionId = promotionId,
                CategoryId = categoryId
            });
        }

        public async Task RemoveAllCategoriesForPromotionAsync(int promotionId)
        {
                        await _context.PromotionCategories
                   .Where(pc => pc.PromotionId == promotionId)
                   .ExecuteDeleteAsync();
        }

        public async Task RemoveCategoryFromPromotionAsync(int promotionId, int categoryId)
        {
            var promoCategory = await _context.PromotionCategories
                .FirstOrDefaultAsync(pc => pc.PromotionId == promotionId && pc.CategoryId == categoryId);

            if (promoCategory != null)
                _context.PromotionCategories.Remove(promoCategory);
        }

    }
}
