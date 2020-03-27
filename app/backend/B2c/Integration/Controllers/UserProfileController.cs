using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleWebApp.B2c.Integration.Models;

namespace SampleWebApp.B2c.Integration.Controllers
{
    public class UserProfileController : IntegrationControllerBase<UserProfileRequest>
    {

        public UserProfileController(ILogger<UserProfileController> logger) : base(logger)
        {
        }

        public override IActionResult Post(UserProfileRequest request)
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
