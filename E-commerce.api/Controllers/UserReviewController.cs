using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.DTOs.ReviewDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserReviewController : ControllerBase
    {
        private readonly IUserReviewService _service;

        public UserReviewController(IUserReviewService service)
        {
            _service = service;
        }




        // ========================= REVIEWS FOR PRODUCT ITEM =========================
        // GET: api/userreview/productitem/12
        [HttpGet("productitem/{productItemId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetForProductItem(int productItemId)
        {
            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");
        
            var reviews = await _service.GetReviewsForProductItemAsync(productItemId);
            return Ok(reviews); 
        }




        // ========================= RECENT REVIEWS FOR PRODUCT =========================
        // GET: api/userreview/product/5/recent?limit=10
        [HttpGet("product/{productId:int}/recent")]
        [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetRecentForProduct(int productId,[FromQuery] int limit = 10)
        {
            if (productId <= 0)
                return BadRequest("Invalid product ID.");

            var reviews = await _service.GetRecentReviewsForProductAsync(productId, limit);
            return Ok(reviews);
        }



        // ========================= REVIEWS WITH USER =========================
        // GET: api/userreview/productitem/12/with-user
        [HttpGet("productitem/{productItemId:int}/with-user")]
        [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetWithUser(int productItemId)
        {
            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            var reviews = await _service.GetReviewsWithUserAsync(productItemId);
            return Ok(reviews);
        }





        // ========================= REVIEWS BY USER =========================
        // GET: api/userreview/user/7
        [HttpGet("user/{userId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByUser(int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid user ID.");

            var reviews = await _service.GetReviewsByUserAsync(userId);
            return Ok(reviews);
        }




        // ========================= SINGLE USER REVIEW =========================
        // GET: api/userreview/user/7/productitem/12
        [HttpGet("user/{userId:int}/productitem/{productItemId:int}")]
        [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewDto>> GetUserReview(int userId, int productItemId)
        {
            if (userId <= 0)
                return BadRequest("Invalid user ID.");
            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            var review = await _service.GetUserReviewAsync(userId, productItemId);
            if (review == null) return NotFound();

            return Ok(review);
        }




        // ========================= TOP RATED PRODUCTS =========================
        // GET: api/userreview/top-rated?limit=10
        [HttpGet("top-rated")]
        [ProducesResponseType(typeof(IEnumerable<TopRatedProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TopRatedProductDto>>> GetTopRated([FromQuery] int limit = 10)
        {
            if (limit <= 0)
                return BadRequest("Limit must be greater than zero.");

            var items = await _service.GetTopRatedProductsAsync(limit);
            return Ok(items);
        }

    }

}
