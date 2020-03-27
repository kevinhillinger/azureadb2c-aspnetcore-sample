using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleWebApp.Controllers
{
    [ApiController]
    [Route("b2c/[controller]")]
    public class RolesController : ControllerBase
    {

        private readonly ILogger<ProfileController> _logger;

        public RolesController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get(Guid userId)
        {
            return new string[] {};
        }
    }
}
