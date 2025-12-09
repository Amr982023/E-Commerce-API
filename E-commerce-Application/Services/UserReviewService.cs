using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.DTOs.ReviewDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using E_commerce_Application.Mapping;

namespace E_commerce_Application.Services
{
    public class UserReviewService : IUserReviewService
    {
        private readonly IUnitOfWork _uow;

        public UserReviewService(IUnitOfWork uow)
        {
            _uow = uow;
        }

   
        // Get all reviews for a product item
        public async Task<IEnumerable<ReviewDto>> GetReviewsForProductItemAsync(int productItemId)
        {
            var reviews = await _uow.UserReviews.GetReviewsForProductAsync(productItemId);
            return reviews.Select(UserReviewMapping.MapToReviewDto);
        }

        // Get recent reviews for a product (any productItem belonging to this product)
        public async Task<IEnumerable<ReviewDto>> GetRecentReviewsForProductAsync(int productId, int limit = 10)
        {
            var reviews = await _uow.UserReviews.GetRecentReviewsAsync(productId, limit);
            return reviews.Select(UserReviewMapping.MapToReviewDto);
        }

        // Get reviews that include user navigation
        public async Task<IEnumerable<ReviewDto>> GetReviewsWithUserAsync(int productItemId)
        {
            var reviews = await _uow.UserReviews.GetReviewsWithUserAsync(productItemId);
            return reviews.Select(UserReviewMapping.MapToReviewDto);
        }

        // Get reviews written by user
        public async Task<IEnumerable<ReviewDto>> GetReviewsByUserAsync(int userId)
        {
            var reviews = await _uow.UserReviews.GetReviewsByUserAsync(userId);
            return reviews.Select(UserReviewMapping.MapToReviewDto);
        }

        // Get specific review (user + productItem)
        public async Task<ReviewDto?> GetUserReviewAsync(int userId, int productItemId)
        {
            var review = await _uow.UserReviews.GetUserReviewAsync(userId, productItemId);
            return review == null ? null : UserReviewMapping.MapToReviewDto(review);
        }

        // Get top rated product items
        public async Task<IEnumerable<TopRatedProductDto>> GetTopRatedProductsAsync(int limit = 10)
        {
            var items = await _uow.UserReviews.GetTopRatedProductsAsync(limit);

            return items.Select(pi => new TopRatedProductDto
            {
                ProductItemId = pi.Id,
                SKU = pi.SKU,
                ProductImage = pi.ProductImage,
                Price = pi.Price
            });
        }

    }

}
