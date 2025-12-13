using E_commerce_Application.DTOs.ProductDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }



        // ========================= GET BY ID (SIMPLE) =========================
        // GET: api/product/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<ActionResult<ProductDetailsDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product ID.");

            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();

            return Ok(product);
        }



        // ========================= GET FULL DETAILS =========================
        // GET: api/product/5/details
        [HttpGet("{id:int}/details")]
        [ProducesResponseType(typeof(ProductDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDetailsDto>> GetWithDetails(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product ID.");
            var product = await _service.GetProductWithDetailsAsync(id);
            if (product == null) return NotFound();

            return Ok(product);
        }



        // ========================= SEARCH =========================
        // GET: api/product/search?term=iphone&limit=20
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Search( [FromQuery] string term, [FromQuery] int limit = 20)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term is required.");

            var products = await _service.SearchAsync(term, limit);
            return Ok(products);
        }



        // ========================= BY CATEGORY =========================
        // GET: api/product/category/3
        [HttpGet("category/{categoryId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest("Invalid category ID.");

            var products = await _service.GetByCategoryAsync(categoryId);
            return Ok(products);
        }



        // ========================= BEST SELLING =========================
        // GET: api/product/best-selling?limit=10
        [HttpGet("best-selling")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetBestSelling([FromQuery] int limit = 10)
        {
            var products = await _service.GetBestSellingAsync(limit);
            return Ok(products);
        }



        // ========================= EXISTS =========================
        // GET: api/product/exists/5
        [HttpGet("exists/{productId:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Exists(int productId)
        {
            if (productId <= 0)
                return BadRequest("Invalid product ID.");

            var exists = await _service.ExistsAsync(productId);
            return Ok(exists);
        }

    }

}
