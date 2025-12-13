using E_commerce_Application.DTOs.ProductCategoryDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _categoryService;

        public ProductCategoryController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/productcategory/parents
        [HttpGet("parents")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetParents()
        {
            var parents = await _categoryService.GetParentsAsync();
            return Ok(parents); 
        }



        // GET: api/productcategory/{parentId}/children
        [HttpGet("{parentId:int}/children")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetChildren(int parentId)
        {
            var children = await _categoryService.GetChildrenAsync(parentId);
            return Ok(children);
        }




        // GET: api/productcategory/{categoryId}/has-children
        [HttpGet("{categoryId:int}/has-children")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> HasChildren(int categoryId)
        {
            var has = await _categoryService.HasChildrenAsync(categoryId);
            return Ok(has);
        }



        // GET: api/productcategory/{categoryId}/has-products
        [HttpGet("{categoryId:int}/has-products")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> HasProducts(int categoryId)
        {
            var has = await _categoryService.HasProductsAsync(categoryId);
            return Ok(has);
        }



        // GET: api/productcategory/search?term=shoes
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Search([FromQuery] string term)
        {
            var results = await _categoryService.SearchAsync(term ?? string.Empty);
            return Ok(results);
        }



        // GET: api/productcategory/tree
        [HttpGet("tree")]
        [ProducesResponseType(typeof(IEnumerable<ProductCategoryTreeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductCategoryTreeDto>>> GetTree()
        {
            var tree = await _categoryService.GetCategoryTreeAsync();
            return Ok(tree);
        }

    }
}
