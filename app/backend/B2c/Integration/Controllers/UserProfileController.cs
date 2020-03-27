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
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(ILogger<UserProfileController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] UserProfileRequest request)
        {
            if (!request.IsValid())
            {
                return BadRequest("id or email address is required");
            }

            if (!TryGetUserProfile(request.Email, out var userProfile))
            {
                return NotFound();
            }

            return Ok(new UserProfileResponse());
        }

        private bool TryGetUserProfile(string email, out UserProfile userProfile) 
        {
            userProfile = new UserProfile();
            return true;
        }
    }
}
