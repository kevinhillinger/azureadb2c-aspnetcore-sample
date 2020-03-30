using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleWebApp.B2c.Controllers
{
    [Authorize(AuthenticationSchemes = CertificateAuthenticationDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("b2c/[controller]")]
    public abstract class IntegrationControllerBase<TRequest> : ControllerBase
    {
        protected readonly ILogger<IntegrationControllerBase<TRequest>> logger;

        public IntegrationControllerBase(ILogger<IntegrationControllerBase<TRequest>> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public abstract IActionResult Post([FromBody] TRequest request);
    }
}