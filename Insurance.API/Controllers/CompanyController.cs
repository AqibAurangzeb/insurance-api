using Insurance.Domain.Abstractions;
using Insurance.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.API.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCompanyResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var result = _companyService.GetCompany(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError("error: {message}", ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/claims")]
        [ProducesResponseType(typeof(GetCompanyClaimsResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCompanyClaims(int id)
        {
            try
            {
                var result = _companyService.GetCompanyClaims(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "company must exist")
                {
                    return BadRequest(ex.Message);
                }

                _logger.LogError("error: {message}", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
