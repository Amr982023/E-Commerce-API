using E_commerce_Application.DTOs.PaymentMethodDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _service;

        public PaymentMethodController(IPaymentMethodService service)
        {
            _service = service;
        }

        // GET: api/paymentmethod/byaccount/5
        [HttpGet("byaccount/{accountId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PaymentMethodDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PaymentMethodDto>>> GetByAccount(int accountId)
        {
            var methods = await _service.GetByAccountAsync(accountId);
            return Ok(methods);
        }



        // GET: api/paymentmethod/account/5/valid
        [HttpGet("account/{accountId:int}/valid")]
        [ProducesResponseType(typeof(IEnumerable<PaymentMethodDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PaymentMethodDto>>> GetValidByAccount(int accountId)
        {
            var methods = await _service.GetValidByAccountAsync(accountId);
            return Ok(methods);
        }



        // GET: api/paymentmethod/account/5/default
        [HttpGet("account/{accountId:int}/default")]
        [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentMethodDto>> GetDefault(int accountId)
        {
            var method = await _service.GetDefaultAsync(accountId);
            if (method == null) return NotFound();
            return Ok(method);
        }



        // GET: api/paymentmethod/provider/stripe
        [HttpGet("provider/{provider}")]
        [ProducesResponseType(typeof(IEnumerable<PaymentMethodDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PaymentMethodDto>>> GetByProvider(string provider)
        {
            var methods = await _service.GetByProviderAsync(provider);
            return Ok(methods);
        }



        // GET: api/paymentmethod/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentMethodDto>> GetById(int id)
        {
            var method = await _service.GetByIdWithDetailsAsync(id);
            if (method == null) return NotFound();
            return Ok(method);
        }




        // POST: api/paymentmethod
        [HttpPost]
        [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentMethodDto>> Create([FromBody] CreatePaymentMethodDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }





        // PUT: api/paymentmethod/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentMethodDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _service.UpdateAsync(id, model);
            if (!success) return NotFound();

            return NoContent();
        }



        // DELETE: api/paymentmethod/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }




        // PUT: api/paymentmethod/account/5/set-default/12
        [HttpPut("account/{accountId:int}/set-default/{paymentMethodId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetDefault(int accountId, int paymentMethodId)
        {
            var success = await _service.SetDefaultAsync(accountId, paymentMethodId);
            if (!success) return BadRequest("Failed to set default payment method (maybe doesn't belong to account).");

            return NoContent();
        }




        // GET: api/paymentmethod/account/5/belongs/12
        [HttpGet("account/{accountId:int}/belongs/{paymentMethodId:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> BelongsToAccount(int accountId, int paymentMethodId)
        {
            var belongs = await _service.BelongsToAccountAsync(accountId, paymentMethodId);
            return Ok(belongs);
        }
    }
}
