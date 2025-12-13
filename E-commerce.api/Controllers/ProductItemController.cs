using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.DTOs.VariationOptionDTOs;
using E_commerce_Core.DTOS;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemService _service;

        public ProductItemController(IProductItemService service)
        {
            _service = service;
        }

        // ========================= GET BY ID WITH DETAILS =========================
        // GET: api/productitem/5/details
        [HttpGet("{id:int}/details")]
        [ProducesResponseType(typeof(ProductItemDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductItemDetailsDto>> GetByIdWithDetails(int id)
        {
            var item = await _service.GetByIdWithDetailsAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }





        // ========================= GET ALL SKUs FOR PRODUCT =========================
        // GET: api/productitem/product/10
        [HttpGet("product/{productId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ProductItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductItemDto>>> GetByProductId(int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid productId.");

            var items = await _service.GetByProductIdAsync(productId);
            return Ok(items); // even if []
        }



        // ========================= GET ITEMS BY OPTIONS =========================
        // POST: api/productitem/product/10/options
        // Body: { "optionIds": [1,2,3] }
        [HttpPost("product/{productId:int}/options")]
        [ProducesResponseType(typeof(IEnumerable<ProductItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductItemDto>>> GetByOptions(int productId,[FromBody] OptionIdsDto model)
        {
            if (model == null || model.OptionIds == null || model.OptionIds.Count == 0)
                return BadRequest("OptionIds are required.");

            var items = await _service.GetByOptionsAsync(productId, model.OptionIds);
            if (items == null) return NotFound();

            return Ok(items);
        }



        // ========================= STOCK =========================
        // GET: api/productitem/5/stock?qty=2
        [HttpGet("stock/{productItemId:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> IsInStock(int productItemId, [FromQuery] int qty)
        {
            if (qty <= 0) 
                return BadRequest("Quantity must be greater than zero.");

            var inStock = await _service.IsInStockAsync(productItemId, qty);
            return Ok(inStock);
        }



        // PUT: api/productitem/5/stock/decrease?qty=2
        [HttpPut("{productItemId:int}/stock/decrease")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DecreaseStock(int productItemId, [FromQuery] int qty)
        {
            if (qty <= 0) return BadRequest("Quantity must be greater than zero.");

            var success = await _service.DecreaseStockAsync(productItemId, qty);
            if (!success) return BadRequest("Not enough stock.");

            return NoContent();
        }




        // PUT: api/productitem/5/stock/increase?qty=2
        [HttpPut("{productItemId:int}/stock/increase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> IncreaseStock(int productItemId, [FromQuery] int qty)
        {
            if (qty <= 0) return BadRequest("Quantity must be greater than zero.");

            await _service.IncreaseStockAsync(productItemId, qty);
            return NoContent();
        }




        // ========================= PRICE =========================
        // GET: api/productitem/5/price
        [HttpGet("price/{productItemId:int}")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetPrice(int productItemId)
        {
            var price = await _service.GetCurrentPriceAsync(productItemId);
            return Ok(price);
        }




        // ========================= IMAGES =========================
        // GET: api/productitem/5/images
        [HttpGet("images/{productItemId:int}")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetImages(int productItemId)
        {
            var images = await _service.GetImagesAsync(productItemId);
            return Ok(images);
        }


        // ========================= VALIDATION =========================
        // GET: api/productitem/5/belongs/10
        [HttpGet("{productItemId:int}/belongs/{productId:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> BelongsToProduct(int productItemId, int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid productId.");
            if (productItemId <= 0)
                return BadRequest("Invalid productItemId.");

            var belongs = await _service.BelongsToProductAsync(productItemId, productId);
            return Ok(belongs);
        }


        // ========================= AVAILABLE COLORS =========================
        // GET: api/productitem/product/10/colors
        [HttpGet("product/{productId:int}/colors")]
        [ProducesResponseType(typeof(IEnumerable<E_commerce_Application.DTOs.VariationOptionDTOs.VariationOptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<E_commerce_Application.DTOs.VariationOptionDTOs.VariationOptionDto>>> GetAvailableColors(int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid productId.");

            var colors = await _service.GetAvailableColorsAsync(productId);
            return Ok(colors);
        }


        // ========================= STOCK SUMMARY =========================
        // GET: api/productitem/product/10/stock-summary
        [HttpGet("product/{productId:int}/stock-summary")]
        [ProducesResponseType(typeof(ProductItemStockDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductItemStockDto>> GetStockSummary(int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid productId.");
            var summary = await _service.GetStockSummaryAsync(productId);
            return Ok(summary);
        }


        // ========================= BULK ADD =========================
        // POST: api/productitem/bulk
        [HttpPost("bulk")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BulkAdd([FromBody] IEnumerable<CreateProductItemDto> items)
        {
            if (items == null || !items.Any())
                return BadRequest("Items are required.");

            await _service.AddProductItemsAsync(items);
            return NoContent();
        }

    }

}
