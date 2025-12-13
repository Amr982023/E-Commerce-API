using E_commerce_Application.DTOs.OrderStatusDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;

        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }


        // ===================== GET ALL STATUS =====================
        // GET: api/orderstatus
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderStatusDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderStatusDto>>> GetAll()
        {
            var statuses = await _orderStatusService.GetAllAsync();
            return Ok(statuses);
        }

    }
}
