using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWebApp.B2C.Models;

namespace SampleWebApp.Controllers
{
    [ApiController]
    [Route("b2c/[controller]")]
    public class UserProfileController : ControllerBase
    {

        private readonly ILogger<ProfileController> _logger;

        public UserProfileController(ILogger<ProfileController> logger)
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

            if (!TryGetUserProfile(request.EmailAddress, out var userProfile))
            {
                return NotFound();
            }

            return Ok(new UserProfileResponse());
        }

        private bool TryGetUserProfile(string emailAddress, out UserProfile userProfile) 
        {
            userProfile = new UserProfile();
            return true;
        }
    }
}
