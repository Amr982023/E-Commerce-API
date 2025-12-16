using E_commerce_Application.DTOs.UserDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ManageUsers")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }




        // ========================= USER WITH DETAILS =========================
        // GET: api/user/5/details
        [HttpGet("{userId:int}/details")]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDetailsDto>> GetWithDetails(int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid user ID.");

            var user = await _service.GetUserWithDetailsAsync(userId);
            if (user == null) return NotFound();

            return Ok(user);
        }



        // ========================= GET BY EMAIL =========================
        // GET: api/user/by-email?email=test@mail.com
        [HttpGet("by-email")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email is required.");

            var user = await _service.GetByEmailAsync(email);
            if (user == null) return NotFound();

            return Ok(user);
        }




        // ========================= GET BY PHONE =========================
        // GET: api/user/by-phone?phone=01000000000
        [HttpGet("by-phone")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetByPhone([FromQuery] string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return BadRequest("Phone is required.");

            var user = await _service.GetByPhoneAsync(phone);
            if (user == null) return NotFound();

            return Ok(user);
        }



        // ========================= GET BY ACCOUNT ID =========================
        // GET: api/user/by-account/7
        [HttpGet("by-account/{accountId:int}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> GetByAccountId(int accountId)
        {
            if (accountId <= 0)
                return BadRequest("Invalid account ID.");

            var user = await _service.GetByAccountIdAsync(accountId);
            if (user == null) return NotFound();

            return Ok(user);
        }



        // ========================= SEARCH USERS =========================
        // GET: api/user/search?name=ahmed&email=&phone=
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Search([FromQuery] string? name, [FromQuery] string? email, [FromQuery] string? phone)
        {
            var users = await _service.SearchUsersAsync(name, email, phone);
            return Ok(users);
        }

    }

}
