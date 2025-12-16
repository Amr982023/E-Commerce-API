using E_commerce_Application.DTOs.VariationDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VariationController : ControllerBase
    {
        private readonly IVariationService _service;

        public VariationController(IVariationService service)
        {
            _service = service;
        }



        // ========================= GET BY ID =========================
        // GET: api/variation/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(VariationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VariationDto>> GetById(int id)
        {
            if (id <= 0) return 
                    BadRequest("Invalid variation ID.");

            var variation = await _service.GetByIdAsync(id);
            if (variation == null) return NotFound();

            return Ok(variation);
        }




        // ========================= GET WITH OPTIONS =========================
        // GET: api/variation/5/options
        [HttpGet("{variationId:int}/options")]
        [ProducesResponseType(typeof(VariationWithOptionsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VariationWithOptionsDto>> GetWithOptions(int variationId)
        {
            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            var variation = await _service.GetWithOptionsAsync(variationId);
            if (variation == null) return NotFound();

            return Ok(variation);
        }



        // ========================= VARIATIONS FOR PRODUCT ITEM =========================
        // GET: api/variation/productitem/25
        [HttpGet("productitem/{productItemId:int}")]
        [ProducesResponseType(typeof(IEnumerable<VariationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VariationDto>>> GetForProductItem(int productItemId)
        {
            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            var variations = await _service.GetVariationsForProductItemAsync(productItemId);
            return Ok(variations);
        }



        // ========================= OPTIONS FOR PRODUCT + VARIATION =========================
        // GET: api/variation/product/10/variation/5/options
        [HttpGet("product/{productId:int}/variation/{variationId:int}/options")]
        [ProducesResponseType(typeof(IEnumerable<VariationOptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VariationOptionDto>>> GetOptionsForProductVariation(int productId,int variationId)
        {
            if (productId <= 0)
                return BadRequest("Invalid product ID.");

            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            var options = await _service.GetOptionsForProductVariationAsync(productId, variationId);
            return Ok(options);
        }





        // ========================= CHECK VARIATION USED =========================
        // GET: api/variation/5/is-used
        [HttpGet("{variationId:int}/is-used")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> IsUsed(int variationId)
        {
            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            var used = await _service.IsVariationUsedAsync(variationId);
            return Ok(used);
        }




        // ========================= RENAME VARIATION =========================
        // PUT: api/variation/5/rename?name=Color
        [HttpPut("{variationId:int}/rename")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Rename(int variationId,[FromQuery] string name)
        {
            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Variation name cannot be empty.");

            var success = await _service.RenameVariationAsync(variationId, name);
            if (!success) return BadRequest("Invalid variation name.");

            return NoContent();
        }




        // ========================= ADD OPTION =========================
        // POST: api/variation/5/options?value=Red
        [HttpPost("{variationId:int}/options")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOption(int variationId,[FromQuery] string value)
        {
            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            if (string.IsNullOrWhiteSpace(value))
                return BadRequest("Option value cannot be empty.");

            var success = await _service.AddOptionAsync(variationId, value);
            if (!success)
                return BadRequest("Unable to add option.");

            return NoContent();
        }




        // ========================= REMOVE OPTION =========================
        // DELETE: api/variation/options/15
        [HttpDelete("options/{variationOptionId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveOption(int variationOptionId)
        {
            if (variationOptionId <= 0)
                return BadRequest("Invalid variation option ID.");

            await _service.RemoveOptionAsync(variationOptionId);
            return NoContent();
        }



        // ========================= VARIATION TREE FOR PRODUCT =========================
        // GET: api/variation/product/10/tree
        [HttpGet("product/{productId:int}/tree")]
        [ProducesResponseType(typeof(IEnumerable<VariationWithOptionsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VariationWithOptionsDto>>> GetVariationTree(int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid product ID.");

            var tree = await _service.GetVariationTreeForProductAsync(productId);
            return Ok(tree);
        }

    }

}
