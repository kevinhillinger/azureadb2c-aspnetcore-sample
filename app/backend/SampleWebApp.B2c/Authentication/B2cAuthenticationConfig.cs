using System.Collections.Generic;

namespace SampleWebApp.B2c.Authentication
{
    public class B2cAuthenticationConfig
    {
        public const string ConfigurationSectionName = "B2c";

        public string Tenant { get; set; }
        public string ClientId { get; set; }

        public string Policy { get; set; }
        public List<string> Scopes { get; set; }

        public string Authority { get { return $"https://{Tenant}.b2clogin.com/tfp/{Tenant}.onmicrosoft.com/{Policy}/v2.0/"; } }
        
        public string Audience { get { return ClientId; } }
    }
}