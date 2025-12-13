using E_commerce_Application.Dtos.AddressDTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Models;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/address/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AddressDto>> GetById(int id)
        {
            var address = await _addressService.GetAddressByIdAsync(id);
            if (address == null) return NotFound();
            return Ok(address);
        }



        // GET: api/address/Byaccount/5
        [HttpGet("Byaccount/{accountId:int}")]
        [ProducesResponseType(typeof(IEnumerable<AddressDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetByAccountId(int accountId)
        {
            var addresses = await _addressService.GetAddressesByAccountAsync(accountId);
            return Ok(addresses);
        }



        // GET: api/address/account/5/default
        [HttpGet("DefaultAddressByAccount/{accountId:int}")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AddressDto>> GetDefault(int accountId)
        {
            var address = await _addressService.GetDefaultAddressAsync(accountId);
            if (address == null) return NotFound();
            return Ok(address);
        }



        // POST: api/address
        [HttpPost]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddressDto>> Create([FromBody] CreateAddressDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _addressService.CreateAddressAsync(model);
            if (created == null) return BadRequest("Failed to create address");
          
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }



        // PUT: api/address/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _addressService.UpdateAddressAsync(id, model);
            if (!success) return NotFound();

            return Ok("Updated Successfully");
        }



        // DELETE: api/address/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _addressService.DeleteAddressAsync(id);
            if (!success) return NotFound();

            return Ok("Deleted Successfully");
        }



        // PUT: api/address/account/5/set-default/12
        [HttpPut("account/{accountId:int}/Set-Default/{addressId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetDefault(int accountId, int addressId)
        {
            // service handles validation; you can add extra checks if needed
            var success = await _addressService.SetDefaultAddressAsync(accountId, addressId);
            if (!success) return BadRequest("Failed to set default address");

            return Ok("Default address updated");
        }

    }

}
