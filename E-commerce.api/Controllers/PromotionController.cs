using E_commerce_Application.DTOs.ProductCategoryDTOs;
using E_commerce_Application.DTOs.PromotionDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _service;

        public PromotionController(IPromotionService service)
        {
            _service = service;
        }


        // ========================= GET ACTIVE PROMOTIONS =========================
        // GET: api/promotion/active
        [HttpGet("active")]
        [ProducesResponseType(typeof(IEnumerable<PromotionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetActive()
        {
            var promotions = await _service.GetActiveAsync();
            return Ok(promotions);
        }




        // ========================= BY CATEGORY =========================
        // GET: api/promotion/category/3
        [HttpGet("category/{categoryId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PromotionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetForCategory(int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest("Invalid categoryId.");

            var promotions = await _service.GetForCategoryAsync(categoryId);
            return Ok(promotions);
        }



        // ========================= BY PRODUCT =========================
        // GET: api/promotion/product/10
        [HttpGet("product/{productId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PromotionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetForProduct(int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid productId.");

            var promotions = await _service.GetForProductAsync(productId);
            return Ok(promotions);
        }




        // ========================= BY PRODUCT ITEM (SKU) =========================
        // GET: api/promotion/productitem/25
        [HttpGet("productitem/{productItemId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PromotionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetForProductItem(int productItemId)
        {
            if (productItemId <= 0)
                return BadRequest("Invalid productItemId.");

            var promotions = await _service.GetForProductItemAsync(productItemId);
            return Ok(promotions);
        }



        // ========================= BEST DISCOUNT RATE =========================
        // GET: api/promotion/productitem/25/best-rate
        [HttpGet("productitem/{productItemId:int}/best-rate")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<decimal>> GetBestDiscountRate(int productItemId)
        {
            if (productItemId <= 0)
                return BadRequest("Invalid productItemId.");
            var rate = await _service.GetBestDiscountRateForProductItemAsync(productItemId);
            return Ok(rate);
        }



        // ========================= APPLY BEST DISCOUNT =========================
        // POST: api/promotion/productitem/25/apply
        // Body: { "basePrice": 1000 }
        [HttpPost("productitem/{productItemId:int}/apply")]
        [ProducesResponseType(typeof(ApplyPromotionResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApplyPromotionResultDto>> ApplyBestDiscount( int productItemId)
        {
          
            if (productItemId <= 0)
                return BadRequest("Invalid productItemId.");
            
            var result = await _service.ApplyBestDiscountAsync(productItemId);
            return Ok(result);
        }



        // ========================= CREATE =========================
        // POST: api/promotion
        [HttpPost]
        [ProducesResponseType(typeof(PromotionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PromotionDto>> Create([FromBody] PromotionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetActive), new { id = created.Id }, created);
        }



        // ========================= UPDATE =========================
        // PUT: api/promotion/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] PromotionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest("Invalid promotion id.");

            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }



        // ========================= DELETE =========================
        // DELETE: api/promotion/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid promotion id.");

            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }



        // ========================= SET CATEGORIES =========================
        // PUT: api/promotion/5/categories
        // Body: { "categoryIds": [1,2,3] }
        [HttpPut("{promotionId:int}/categories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetCategories(int promotionId,[FromBody] CategoryIdsDto model)
        {
            if (model == null || model.CategoryIds == null)
                return BadRequest("CategoryIds are required.");
            if (promotionId <= 0)
                return BadRequest("Invalid promotion id.");

            await _service.SetCategoriesForPromotionAsync(promotionId, model.CategoryIds);
            return NoContent();
        }

    }

}
