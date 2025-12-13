using E_commerce_Application.Dtos.AccountDTOs;
using E_commerce_Application.DTOs.AccountDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;


namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // ================== Register ==================
        [EnableRateLimiting("fixed")]
        [HttpPost("register")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> Register([FromBody] RegisterAccountDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _accountService.RegisterAsync(model);
            if (account == null)
                return BadRequest("Failed to register account.");

            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }



        // ================== Authenticate ==================
        [EnableRateLimiting("fixed")]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AccountDto>> Login([FromBody] LoginRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _accountService.AuthenticateAsync(model.Username, model.Password);
            if (account == null)
                return Unauthorized("Invalid Username or Password.");

            return Ok(account);
        }




        // ================== Read ==================
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDto>> GetById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }




        [HttpGet("ByUsername/{username}")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDto>> GetByUsername(string username)
        {
            var account = await _accountService.GetByUsernameAsync(username);
            if (account == null)
                return NotFound();

            return Ok(account);
        }



        [HttpGet("WithDetails/{id:int}")]
        [ProducesResponseType(typeof(AccountWithDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountWithDetailsDto>> GetWithDetails(int id)
        {
            var account = await _accountService.GetWithDetailsAsync(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }




        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();
            return Ok(accounts);
        }





        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AccountDto>>> Search([FromQuery] string? username, [FromQuery] string? email)
        {
            var accounts = await _accountService.SearchAsync(username, email);
            return Ok(accounts);
        }





        // ================== Update / Change Password ==================
        [EnableRateLimiting("fixed")]
        [HttpPut("change-password/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _accountService.ChangePasswordAsync(id, model.OldPassword, model.NewPassword);
            if (!success)
                return BadRequest("Failed to change password. Check old password or account id.");

            return Ok("Password changed successfully");
        }





        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _accountService.UpdateAccountAsync(id, model);
            if (!success)
                return NotFound();

            return NoContent();
        }




        // ================== Delete ==================
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _accountService.DeleteAccountAsync(id);
            if (!success)
                return NotFound();

            return Ok("Deleted Successfully");
        }




        // ================== Validation ==================
        [HttpGet("check-username")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UsernameExists([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Username is required.");

            var exists = await _accountService.UsernameExistsAsync(username);
            return Ok(exists);
        }

    }

}
