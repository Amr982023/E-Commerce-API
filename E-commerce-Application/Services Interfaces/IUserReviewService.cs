using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.DTOs.ReviewDTOs;

namespace E_commerce_Application.Services_Interfaces
{
    public interface IUserReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsForProductItemAsync(int productItemId);
        Task<IEnumerable<ReviewDto>> GetRecentReviewsForProductAsync(int productId, int limit = 10);
        Task<IEnumerable<ReviewDto>> GetReviewsWithUserAsync(int productItemId);
        Task<IEnumerable<ReviewDto>> GetReviewsByUserAsync(int userId);
        Task<ReviewDto?> GetUserReviewAsync(int userId, int productItemId);
        Task<IEnumerable<TopRatedProductDto>> GetTopRatedProductsAsync(int limit = 10);
    }

}
