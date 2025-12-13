using E_commerce_Application.DTOs.ShippingMethodDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethodService _service;

        public ShippingMethodController(IShippingMethodService service)
        {
            _service = service;
        }



        // ========================= GET ALL =========================
        // GET: api/shippingmethod
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShippingMethodDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShippingMethodDto>>> GetAll()
        {
            var methods = await _service.GetAllAsync();
            return Ok(methods);
        }



        // ========================= GET AVAILABLE ONLY =========================
        // GET: api/shippingmethod/available
        [HttpGet("available")]
        [ProducesResponseType(typeof(IEnumerable<ShippingMethodDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShippingMethodDto>>> GetAvailable()
        {
            var methods = await _service.GetAvailableAsync();
            return Ok(methods);
        }




        // ========================= GET CHEAPEST =========================
        // GET: api/shippingmethod/cheapest
        [HttpGet("cheapest")]
        [ProducesResponseType(typeof(ShippingMethodDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingMethodDto>> GetCheapest()
        {
            var method = await _service.GetCheapestAsync();
            if (method == null) return NotFound();

            return Ok(method);
        }


        // ========================= GET MOST EXPENSIVE =========================
        // GET: api/shippingmethod/most-expensive
        [HttpGet("MostExpensive")]
        [ProducesResponseType(typeof(ShippingMethodDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingMethodDto>> GetMostExpensive()
        {
            var method = await _service.GetMostExpensiveAsync();
            if (method == null) return NotFound();

            return Ok(method);
        }

    }

}
