using Insurance.Domain.Abstractions;
using Insurance.Domain.Entities;
using Insurance.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.API.Controllers
{
    [ApiController]
    [Route("api/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimsService _claimsService;
        private readonly ILogger<ClaimsController> _logger;

        public ClaimsController(ILogger<ClaimsController> logger, IClaimsService claimsService)
        {
            _logger = logger;
            _claimsService = claimsService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetClaimResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetClaim(int id)
        {
            try
            {
                var result = _claimsService.GetClaim(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("error: {message}", ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Claims), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateClaim(int id, Claim claim)
        {
            try
            {
                var result = _claimsService.UpdateClaim(id, claim);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "claim not found")
                {
                    return NotFound();
                }

                _logger.LogError("error: {message}", ex.Message);
                return StatusCode(500);
            }
        }
    }
}