using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce_Application.DTOs.ProductCategoryDTOs;
using Microsoft.AspNetCore.Authorization;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PromotionCategoryController : ControllerBase
    {
        private readonly IPromotionCategoryService _service;

        public PromotionCategoryController(IPromotionCategoryService service)
        {
            _service = service;
        }



        // ========================= ADD SINGLE CATEGORY =========================
        // POST: api/promotioncategory/promotion/5/category/3
        [HttpPost("promotion/{promotionId:int}/category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategoryToPromotion(int promotionId, int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest("Invalid categoryId.");

            if (promotionId <= 0)
                return BadRequest("Invalid promotionId.");

            await _service.AddCategoryToPromotionAsync(promotionId, categoryId);
            return NoContent();
        }




        // ========================= ADD MULTIPLE CATEGORIES =========================
        // POST: api/promotioncategory/promotion/5/categories
        // Body: { "categoryIds": [1,2,3] }
        [HttpPost("promotion/{promotionId:int}/categories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategoriesToPromotion(int promotionId,[FromBody] CategoryIdsDto model)
        {
            if (model == null || model.CategoryIds == null || !model.CategoryIds.Any())
                return BadRequest("CategoryIds are required.");

            if (promotionId <= 0)
                return BadRequest("Invalid promotionId.");

            await _service.AddCategoriesToPromotionAsync(promotionId, model.CategoryIds);
            return NoContent();
        }




        // ========================= REMOVE SINGLE CATEGORY =========================
        // DELETE: api/promotioncategory/promotion/5/category/3
        [HttpDelete("promotion/{promotionId:int}/category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCategoryFromPromotion(int promotionId, int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest("Invalid categoryId.");
            if (promotionId <= 0)
                return BadRequest("Invalid promotionId.");

            await _service.RemoveCategoryFromPromotionAsync(promotionId, categoryId);
            return NoContent();
        }




        // ========================= REMOVE ALL CATEGORIES =========================
        // DELETE: api/promotioncategory/promotion/5/categories
        [HttpDelete("promotion/{promotionId:int}/categories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveAllCategoriesForPromotion(int promotionId)
        {
            if (promotionId <= 0)
                return BadRequest("Invalid promotionId.");

            await _service.RemoveAllCategoriesForPromotionAsync(promotionId);
            return NoContent();
        }



        // ========================= REPLACE CATEGORIES (ADMIN UI) =========================
        // PUT: api/promotioncategory/promotion/5/categories
        // Body: { "categoryIds": [2,4,6] }
        [HttpPut("promotion/{promotionId:int}/categories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReplaceCategoriesForPromotion(int promotionId,[FromBody] CategoryIdsDto model)
        {
            if (model == null || model.CategoryIds == null)
                return BadRequest("CategoryIds are required.");

            if (promotionId <= 0)
                return BadRequest("Invalid promotionId.");

            await _service.ReplaceCategoriesForPromotionAsync(promotionId, model.CategoryIds);
            return NoContent();
        }

    }

}
