using SampleWebApp.B2c.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleWebApp.B2c.Controllers
{
    public class ValidateEmailController : IntegrationControllerBase<ValidateEmailRequest>
    {
        public ValidateEmailController(ILogger<ValidateEmailController> logger) : base(logger)
        {
        }

        public override IActionResult Post([FromBody] ValidateEmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email address is required");
            }

            if (!DoesEmailExist(request.Email))
            {
                return NotFound();
            }

            return Ok(new ValidateEmailResponse { Validated = true, Count = 1 });
        }

        private bool DoesEmailExist(string email)
        {
            // hard coded sample only
            return true;
        }
    }
}
