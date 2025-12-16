using E_commerce_Application.DTOs.CartItemDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _service;

        public ShoppingCartItemController(IShoppingCartItemService service)
        {
            _service = service;
        }



        // ========================= GET ALL ITEMS (BASIC) =========================
        // GET: api/shoppingcartitem/account/5
        [HttpGet("account/{accountId:int}")]
        [ProducesResponseType(typeof(IEnumerable<CartItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int accountId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            var items = await _service.GetItemsAsync(accountId);
            return Ok(items);
        }




        // ========================= GET ALL ITEMS WITH DETAILS =========================
        // GET: api/shoppingcartitem/account/5/details
        [HttpGet("account/{accountId:int}/details")]
        [ProducesResponseType(typeof(IEnumerable<ShoppingCartItemWithDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItemsWithDetails(int accountId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            var items = await _service.GetItemsWithDetailsAsync(accountId);
            return Ok(items);
        }





        // ========================= GET SINGLE ITEM =========================
        // GET: api/shoppingcartitem/account/5/item/12
        [HttpGet("account/{accountId:int}/item/{productItemId:int}")]
        [ProducesResponseType(typeof(CartItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartItemDto>> GetItem(int accountId, int productItemId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            var item = await _service.GetItemAsync(accountId, productItemId);
            if (item == null) return NotFound();

            return Ok(item);
        }




        // ========================= GET SINGLE ITEM WITH DETAILS =========================
        // GET: api/shoppingcartitem/account/5/item/12/details
        [HttpGet("account/{accountId:int}/item/{productItemId:int}/details")]
        [ProducesResponseType(typeof(CartItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShoppingCartItemWithDetailsDto>> GetItemWithDetails(int accountId, int productItemId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            var item = await _service.GetItemWithDetailsAsync(accountId, productItemId);
            if (item == null) return NotFound();

            return Ok(item);
        }




        // ========================= UPDATE QUANTITY =========================
        // PUT: api/shoppingcartitem/account/5/item/12?qty=3
        [HttpPut("account/{accountId:int}/item/{productItemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuantity( int accountId,int productItemId,[FromQuery] int qty)
        {
            if (qty <= 0)
                return BadRequest("Quantity must be greater than zero.");

            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            await _service.UpdateItemQuantityAsync(accountId, productItemId, qty);
            return NoContent();
        }



        // ========================= ITEM TOTAL PRICE =========================
        // GET: api/shoppingcartitem/account/5/item/12/total
        [HttpGet("account/{accountId:int}/item/{productItemId:int}/total")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<decimal>> GetItemTotal( int accountId, int productItemId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            var total = await _service.GetItemTotalPriceAsync(accountId, productItemId);
            return Ok(total);
        }

    }

}
