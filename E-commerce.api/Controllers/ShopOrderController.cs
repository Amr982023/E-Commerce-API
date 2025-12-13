using E_commerce_Application.DTOs.ShopOrderDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopOrderController : ControllerBase
    {
        private readonly IShopOrderService _service;

        public ShopOrderController(IShopOrderService service)
        {
            _service = service;
        }



        // ========================= GET ORDER WITH DETAILS =========================
        // GET: api/shoporder/5/details
        [HttpGet("{orderId:int}/details")]
        [ProducesResponseType(typeof(ShopOrderDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShopOrderDetailsDto>> GetWithDetails(int orderId)
        {
            if (orderId <= 0)
                return BadRequest("Invalid order ID.");

            var order = await _service.GetOrderWithDetailsAsync(orderId);
            if (order == null) return NotFound();

            return Ok(order);
        }




        // ========================= ORDERS FOR ACCOUNT =========================
        // GET: api/shoporder/account/3
        [HttpGet("account/{accountId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ShopOrderSummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ShopOrderSummaryDto>>> GetForAccount(int accountId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            var orders = await _service.GetOrdersForAccountAsync(accountId);
            return Ok(orders);
        }



        // GET: api/shoporder/account/3/paged?page=1&size=10
        [HttpGet("account/{accountId:int}/paged")]
        [ProducesResponseType(typeof(IEnumerable<ShopOrderSummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ShopOrderSummaryDto>>> GetForAccountPaged(int accountId,[FromQuery] int page = 1,[FromQuery] int size = 10)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            var orders = await _service.GetOrdersForAccountPagedAsync(accountId, page, size);
            return Ok(orders);
        }




        // GET: api/shoporder/account/3/last
        [HttpGet("account/{accountId:int}/last")]
        [ProducesResponseType(typeof(ShopOrderSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShopOrderSummaryDto>> GetLastOrder(int accountId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            var order = await _service.GetLastOrderAsync(accountId);
            if (order == null) return NotFound();

            return Ok(order);
        }



        // ========================= STATUS FILTERS =========================
        // GET: api/shoporder/status/1
        [HttpGet("status/{status:int}")]
        [ProducesResponseType(typeof(IEnumerable<ShopOrderSummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ShopOrderSummaryDto>>> GetByStatus(int status)
        {
            if (status <= 0)
                return BadRequest("Invalid status value.");

            enOrderStatus Status = (enOrderStatus)status; 

            var orders = await _service.GetOrdersByStatusAsync(Status);
            return Ok(orders);
        }



        // GET: api/shoporder/pending
        [HttpGet("pending")]
        [ProducesResponseType(typeof(IEnumerable<ShopOrderSummaryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShopOrderSummaryDto>>> GetPending()
        {
            var orders = await _service.GetPendingOrdersAsync();
            return Ok(orders);
        }



        // GET: api/shoporder/range?start=2025-01-01&end=2025-01-31
        [HttpGet("range")]
        [ProducesResponseType(typeof(IEnumerable<ShopOrderSummaryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShopOrderSummaryDto>>> GetInRange([FromQuery] DateTime start,[FromQuery] DateTime end)
        {
            var orders = await _service.GetOrdersInRangeAsync(start, end);
            return Ok(orders);
        }




        // ========================= COMMANDS =========================
        // PUT: api/shoporder/5/cancel
        [HttpPut("{orderId:int}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cancel(int orderId)
        {
            if (orderId <= 0)
                return BadRequest("Invalid order ID.");

            await _service.CancelOrderAsync(orderId);
            return NoContent();
        }




        // PUT: api/shoporder/5/confirm
        [HttpPut("{orderId:int}/confirm")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Confirm(int orderId)
        {
            if (orderId <= 0)
                return BadRequest("Invalid order ID.");

            await _service.ConfirmOrderAsync(orderId);
            return NoContent();
        }




        // PUT: api/shoporder/5/shipping/2
        [HttpPut("{orderId:int}/shipping/{shippingMethodId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetShippingMethod(int orderId, int shippingMethodId)
        {
            if (orderId <= 0)
                return BadRequest("Invalid order ID.");

            if (shippingMethodId <= 0)
                return BadRequest("Invalid shipping method ID.");

            await _service.SetShippingMethodAsync(orderId, shippingMethodId);
            return NoContent();
        }




        // PUT: api/shoporder/5/payment/3
        [HttpPut("{orderId:int}/payment/{paymentMethodId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetPaymentMethod(int orderId, int paymentMethodId)
        {
            if (orderId <= 0)
                return BadRequest("Invalid order ID.");

            if (paymentMethodId <= 0)
                return BadRequest("Invalid payment method ID.");

            await _service.SetPaymentMethodAsync(orderId, paymentMethodId);
            return NoContent();
        }



        // ========================= REPORTS / KPIs =========================
        // GET: api/shoporder/reports/today-sales
        [HttpGet("reports/today-sales")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetTodaySales()
        {
            var total = await _service.GetTodaySalesAsync();
            return Ok(total);
        }



        // GET: api/shoporder/reports/average-order-value
        [HttpGet("reports/average-order-value")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetAverageOrderValue()
        {
            var avg = await _service.GetAverageOrderValueAsync();
            return Ok(avg);
        }
    }

}
