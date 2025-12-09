using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IPromotion : IGenericRepository<Promotion>
    {
        Task<IEnumerable<Promotion>> GetActivePromotionsAsync();

        Task<IEnumerable<Promotion>> GetPromotionsForProductAsync(int productId);

        Task<IEnumerable<Promotion>> GetPromotionsForProductItemAsync(int productItemId);

        Task<IEnumerable<Promotion>> GetPromotionsForCategoryAsync(int categoryId);

        Task<decimal> GetDiscountForProductItemAsync(int productItemId);

        bool IsExpired(Promotion promo);

    }
}
