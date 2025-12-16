using E_commerce_Application.DTOs.ShoppingCartDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;

        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }




        // ========================= GET FULL CART =========================
        // GET: api/shoppingcart/account/5
        [HttpGet("account/{accountId:int}")]
        [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CartDto>> GetCart(int accountId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            var cart = await _service.GetCartAsync(accountId);
            if (cart == null)
                return NotFound();
            return Ok(cart); 
        }




        // ========================= ADD ITEM =========================
        // POST: api/shoppingcart/account/5/item/12?qty=2
        [HttpPost("account/{accountId:int}/item/{productItemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddItem(int accountId, int productItemId,[FromQuery] int qty)
        {
            if (qty <= 0)
                return BadRequest("Quantity must be greater than zero.");

            if(accountId <=0)
                return BadRequest("Invalid account ID.");

            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            await _service.AddItemAsync(accountId, productItemId, qty);
            return NoContent();
        }




        // ========================= UPDATE QUANTITY =========================
        // PUT: api/shoppingcart/account/5/item/12?qty=3
        [HttpPut("account/{accountId:int}/item/{productItemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateItemQuantity( int accountId,int productItemId,[FromQuery] int qty)
        {
            if (qty <= 0)
                return BadRequest("Quantity must be greater than zero.");

            if (accountId <=0)
                return BadRequest("Invalid account ID.");

            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            await _service.UpdateItemQuantityAsync(accountId, productItemId, qty);
            return NoContent();
        }




        // ========================= REMOVE ITEM =========================
        // DELETE: api/shoppingcart/account/5/item/12
        [HttpDelete("account/{accountId:int}/item/{productItemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveItem(int accountId, int productItemId)
        {
            if (accountId <=0)
                return BadRequest("Invalid account ID.");

            if (productItemId <= 0)
                return BadRequest("Invalid product item ID.");

            await _service.RemoveItemAsync(accountId, productItemId);
            return NoContent();
        }





        // ========================= CLEAR CART =========================
        // DELETE: api/shoppingcart/account/5
        [HttpDelete("account/{accountId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClearCart(int accountId)
        {
            if (accountId <=0)
                return BadRequest("Invalid account ID.");

            await _service.ClearCartAsync(accountId);
            return NoContent();
        }

    }

}
