using System.Collections.Generic;

namespace SampleWebApp.B2c.Authentication
{
    public class B2cAuthenticationConfig
    {
        public const string ConfigurationSectionName = "AzureAuthentication";

        public string Tenant { get; set; }
        public string ClientId { get; set; }

        public string Policy { get; set; }
        public List<string> Scopes { get; set; }

        public string Authority { get { return $"https://login.microsoftonline.com/tfp/{Tenant}/{ClientId}/v2.0/"; } }
        
        public string Audience { get { return ClientId; } }
    }
}