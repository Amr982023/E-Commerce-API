using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;

namespace E_commerce_Core.Interfaces
{
    public interface IUserReview : IGenericRepository<UserReview>
    {
        Task<IEnumerable<UserReview>> GetReviewsForProductAsync(int productItemId);
        Task<IEnumerable<UserReview>> GetRecentReviewsAsync(int productId, int limit = 10);
        Task<IEnumerable<UserReview>> GetReviewsWithUserAsync(int productItemId);
        Task<IEnumerable<UserReview>> GetReviewsByUserAsync(int userId);
        Task<UserReview?> GetUserReviewAsync(int userId, int productItemId);
        Task<IEnumerable<ProductItem>> GetTopRatedProductsAsync(int limit = 10);

    }
}
