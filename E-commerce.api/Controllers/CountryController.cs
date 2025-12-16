using E_commerce_Application.Dtos.CountryDTOs;
using E_commerce_Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: api/country
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CountryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            var countries = await _countryService.GetAllAsync();
           
            return Ok(countries);
        }




        // GET: api/country/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            var country = await _countryService.GetByIdAsync(id);
            if (country == null) return NotFound();
            return Ok(country);
        }




        // GET: api/country/by-name/{name}
        [HttpGet("ByName/{name}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryDto>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Name is required.");

            var country = await _countryService.GetByNameAsync(name);
            if (country == null) return NotFound();
            return Ok(country);
        }




        // GET: api/country/by-code/{code}
        [HttpGet("ByCode/{code}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryDto>> GetByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return BadRequest("Code is required.");

            var country = await _countryService.GetByCodeAsync(code);
            if (country == null) return NotFound();
            return Ok(country);
        }

    }
}
