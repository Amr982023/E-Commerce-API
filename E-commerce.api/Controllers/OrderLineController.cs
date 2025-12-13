using E_commerce_Application.Dtos.OrderLineDTOs;
using E_commerce_Application.DTOs.OrderLineDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineService _orderLineService;

        public OrderLineController(IOrderLineService orderLineService)
        {
            _orderLineService = orderLineService;
        }

        // ========================= GET ALL LINES FOR ORDER =========================
        // GET: api/orderline/order/5
        [HttpGet("ByOrder/{orderId:int}")]
        [ProducesResponseType(typeof(IEnumerable<OrderLineDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderLineDto>>> GetByOrderId(int orderId)
        {
            var lines = await _orderLineService.GetByOrderIdAsync(orderId);
          
            return Ok(lines);
        }



        // ========================= GET SINGLE LINE =========================
        // GET: api/orderline/order/5/item/10
        [HttpGet("order/{orderId:int}/item/{productItemId:int}")]
        [ProducesResponseType(typeof(OrderLineDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderLineDto>> GetLine(int orderId, int productItemId)
        {
            var line = await _orderLineService.GetLineAsync(orderId, productItemId);
            return Ok(line);
        }



        // ========================= GET LINE WITH DETAILS =========================
        // GET: api/orderline/order/5/item/10/details
        [HttpGet("order/{orderId:int}/item/{productItemId:int}/details")]
        [ProducesResponseType(typeof(OrderLineDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderLineWithDetailsDto>> GetLineWithDetails(int orderId, int productItemId)
        {
            var line = await _orderLineService.GetLineWithDetailsAsync(orderId, productItemId);
            return Ok(line);
        }



        // ========================= ADD A NEW ORDER LINE =========================
        [HttpPost]
        [ProducesResponseType(typeof(OrderLineDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderLineDto>> Add([FromBody] CreateOrderLineDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var line = await _orderLineService.AddLineAsync(model);

            return CreatedAtAction(
                nameof(GetLine),
                new { orderId = line.ShopOrderId, productItemId = line.ProductItemId },
                line
            );
        }



        // ========================= UPDATE QUANTITY =========================
        // PUT: api/orderline/order/5/item/10?qty=3
        [HttpPut("order/{orderId:int}/item/{productItemId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuantity(int orderId,int productItemId,[FromQuery] int qty)
        {
            if (qty <= 0) return BadRequest("Quantity must be greater than zero.");

            var success = await _orderLineService.UpdateQuantityAsync(orderId, productItemId, qty);
            if (!success) return NotFound();

            return Ok("Quantity updated successfully");
        }




        // ========================= REMOVE AN ORDER LINE =========================
        // DELETE: api/orderline/order/5/item/10
        [HttpDelete("order/{orderId:int}/item/{productItemId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int orderId, int productItemId)
        {
            var success = await _orderLineService.RemoveLineAsync(orderId, productItemId);
            if (!success) return NotFound();

            return Ok("Order line removed successfully");
        }

    }
}
