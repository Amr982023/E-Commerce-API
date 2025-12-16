using E_commerce_Application.DTOs.ProductConfigurationDTOs;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.DTOs.VariationOptionDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductConfigurationController : ControllerBase
    {
        private readonly IProductConfigurationService _service;

        public ProductConfigurationController(IProductConfigurationService service)
        {
            _service = service;
        }

        // ===== GET configurations for a product item =====
        // GET: api/productconfiguration/productitem/123
        [HttpGet("productitem/{productItemId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ProductConfigurationDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductConfigurationDto>>> GetByProductItem(int productItemId)
        {
            var configs = await _service.GetByProductItemAsync(productItemId);
            return Ok(configs);
        }




        // ===== GET variation options available for a product (filters/UI) =====
        // GET: api/productconfiguration/product/45/options
        [HttpGet("product/{productId:int}/options")]
        [ProducesResponseType(typeof(IEnumerable<VariationOptionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VariationOptionDto>>> GetOptionsForProduct(int productId)
        {
            var options = await _service.GetOptionsForProductAsyncService(productId);
            return Ok(options);
        }




        // ===== CHECK existence (does this productItem have this option) =====
        // GET: api/productconfiguration/exists?productItemId=10&variationOptionId=5
        [HttpGet("exists")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Exists([FromQuery] int productItemId, [FromQuery] int variationOptionId)
        {
            var exists = await _service.ExistsAsync(productItemId, variationOptionId);
            return Ok(exists);
        }




        // ===== GET product items that have a specific option =====
        // GET: api/productconfiguration/option/5/items
        [HttpGet("GetItemsByOption/{variationOptionId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ProductItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductItemDto>>> GetProductItemsByOption(int variationOptionId)
        {
            var items = await _service.GetProductItemsByOptionAsync(variationOptionId);
            return Ok(items);
        }




        // ===== Find product item matching all selected options =====
        // POST: api/productconfiguration/product/45/match
        // Body: { "optionIds": [1,2,3] }
     

        [HttpPost("product/{productId:int}/match")]
        [ProducesResponseType(typeof(ProductItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductItemDto>> GetProductItemByOptions(int productId, [FromBody] OptionIdsDto model)
        {
            if (model == null || model.OptionIds == null || model.OptionIds.Count == 0)
                return BadRequest("OptionIds are required.");

            var item = await _service.GetProductItemByOptionsAsync(productId, model.OptionIds);
            if (item == null) return NotFound();
            return Ok(item);
        }



        // ===== Add configurations to a product item (assign option ids) =====
        // POST: api/productconfiguration/productitem/123/configurations
        // Body: { "optionIds": [1,2,3] }
        [HttpPost("productitem/{productItemId:int}/configurations")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddConfigurations(int productItemId, [FromBody] OptionIdsDto model)
        {
            if (model == null || model.OptionIds == null || model.OptionIds.Count == 0)
                return BadRequest("OptionIds are required.");

            await _service.AddConfigurationsAsync(productItemId, model.OptionIds);
            return NoContent();
        }



        // ===== Remove all configurations for a product item =====
        // DELETE: api/productconfiguration/productitem/123/configurations
        [HttpDelete("productitem/{productItemId:int}/configurations")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveConfigurations(int productItemId)
        {   
            var success = await _service.RemoveConfigurationsAsync(productItemId);
            if (!success) return NotFound();

            return NoContent();
        }

    }
}
