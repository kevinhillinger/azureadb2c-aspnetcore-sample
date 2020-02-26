using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SampleWebApp.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController(IOptions<AzureAdB2COptions> b2cOptions)
        {
            AzureAdB2COptions = b2cOptions.Value;
        }

        public AzureAdB2COptions AzureAdB2COptions { get; set; }

        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, 
                OpenIdConnectDefaults.AuthenticationScheme
            );
        }

        [Route("reset")]
        [HttpGet]
        public IActionResult ResetPassword()
        {
            var properties = new AuthenticationProperties { RedirectUri = "/home" };
            properties.Items[AzureAdB2COptions.PolicyAuthenticationProperty] = AzureAdB2COptions.ResetPasswordPolicyId;
            return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [Route("edit")]
        [HttpGet]
        public IActionResult EditProfile()
        {
            var properties = new AuthenticationProperties { RedirectUri = "/home" };
            properties.Items[AzureAdB2COptions.PolicyAuthenticationProperty] = AzureAdB2COptions.EditProfilePolicyId;
            return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
