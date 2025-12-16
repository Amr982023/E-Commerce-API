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
    public class VariationOptionController : ControllerBase
    {
        private readonly IVariationOptionService _service;

        public VariationOptionController(IVariationOptionService service)
        {
            _service = service;
        }



        // ========================= OPTIONS FOR VARIATION =========================
        // GET: api/variationoption/variation/5
        [HttpGet("variation/{variationId:int}")]
        [ProducesResponseType(typeof(IEnumerable<VariationOptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VariationOptionDto>>> GetForVariation(int variationId)
        {
            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            var options = await _service.GetOptionsForVariationAsync(variationId);
            return Ok(options); 
        }




        // ========================= OPTIONS FOR PRODUCT + VARIATION =========================
        // GET: api/variationoption/product/10/variation/5
        [HttpGet("product/{productId:int}/variation/{variationId:int}")]
        [ProducesResponseType(typeof(IEnumerable<VariationOptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VariationOptionDto>>> GetForProduct(int productId,int variationId)
        {
            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            if (productId <= 0)
                return BadRequest("Invalid product ID.");

            var options = await _service.GetOptionsForProductAsync(productId, variationId);
            return Ok(options);
        }




        // ========================= OPTIONS FOR PRODUCT ITEM (SKU) =========================
        // GET: api/variationoption/OptionsForProductItem/25
        [HttpGet("OptionsForProductItem/{productItemId:int}")]
        [ProducesResponseType(typeof(IEnumerable<VariationOptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VariationOptionDto>>> GetForProductItem(int productItemId)
        {
            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            var options = await _service.GetOptionsForProductItemAsync(productItemId);
            return Ok(options);
        }





        // ========================= CHECK OPTION EXISTS =========================
        // GET: api/variationoption/exists?variationId=5&value=Red
        [HttpGet("exists")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> OptionExists([FromQuery] int variationId,[FromQuery] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return BadRequest("Option value is required.");

            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            var exists = await _service.OptionExistsAsync(variationId, value);
            return Ok(exists);
        }




        // ========================= CHECK OPTION IS USED =========================
        // GET: api/variationoption/5/is-used
        [HttpGet("{optionId:int}/is-used")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> IsUsed(int optionId)
        {
            if (optionId <= 0)
                return BadRequest("Invalid option ID.");

            var used = await _service.IsOptionUsedAsync(optionId);
            return Ok(used);
        }



        // ========================= OPTIONS DTO (DIRECT FROM REPO) =========================
        // GET: api/variationoption/variation/5/dto
        [HttpGet("variation/{variationId:int}/dto")]
        [ProducesResponseType(typeof(IEnumerable<VariationOptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VariationOptionDto>>> GetOptionsDto(int variationId)
        {
            if (variationId <= 0)
                return BadRequest("Invalid variation ID.");

            var options = await _service.GetOptionsDtoForVariationAsync(variationId);
            return Ok(options);
        }

    }

}
