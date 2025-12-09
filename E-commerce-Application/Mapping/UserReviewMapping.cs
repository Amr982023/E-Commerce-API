using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ReviewDTOs;
using E_commerce_Core.Models;

namespace E_commerce_Application.Mapping
{
    internal static class UserReviewMapping
    {
        public static ReviewDto MapToReviewDto(UserReview review)
        {
            var productItem = review.OrderLine?.ProductItem;

            return new ReviewDto
            {
                Id = review.Id,
                UserId = review.UserId,
                UserName = review.User != null
                    ? $"{review.User.FirstName} {review.User.LastName}"
                    : null,

                ProductItemId = review.OrderLine?.ProductItemId ?? 0,
                SKU = productItem?.SKU,
                ProductName = productItem?.Product?.Name,

                RatingValue = review.RatingValue,
                Comment = review.Comment
            };
        }

    }
}
