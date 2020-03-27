using backend.B2c.Integration.Models;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWebApp.B2c.Integration.Models;

namespace SampleWebApp.B2c.Integration.Controllers
{
    [Authorize(AuthenticationSchemes = CertificateAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("b2c/[controller]")]
    public class ValidateEmailController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;

        public ValidateEmailController(ILogger<UserProfileController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email address is required");
            }

            if (!DoesEmailExist(email))
            {
                return NotFound();
            }

            return Ok(new EmailVerifyResponse { Exists = true, Count = 1 });
        }

        private bool DoesEmailExist(string email)
        {
            return true;
        }
    }
}
