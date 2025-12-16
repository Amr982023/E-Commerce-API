using E_commerce_Application.DTOs.PaymentTypeDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }



        // GET: api/paymenttype
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentTypeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PaymentTypeDto>>> GetAll()
        {
            var types = await _paymentTypeService.GetAllAsync();
            return Ok(types);
        }




        // GET: api/paymenttype/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PaymentTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentTypeDto>> GetById(int id)
        {
            var type = await _paymentTypeService.GetByIdAsync(id);
            if (type == null) return NotFound();
            return Ok(type);
        }



        // GET: api/paymenttype/byname/{name}
        [HttpGet("byname/{name}")]
        [ProducesResponseType(typeof(PaymentTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentTypeDto>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Name is required.");

            var type = await _paymentTypeService.GetByNameAsync(name);
            if (type == null) return NotFound();
            return Ok(type);
        }
    }
}
