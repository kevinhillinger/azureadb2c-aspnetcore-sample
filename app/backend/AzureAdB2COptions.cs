using System.Linq;

namespace SampleWebApp
{
  public class AzureAdB2COptions
    {
        public const string PolicyAuthenticationProperty = "Policy";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Tenant { get; set; }
        public string Authority => $"https://{Tenant.Split('.').First()}.b2clogin.com/tfp/{Tenant}/{DefaultPolicy}/v2.0";
        public string ResetPasswordPolicyId { get; set; }
        public string EditProfilePolicyId { get; set; }
        public string RedirectUri { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string SignInPolicyId { get; set; }
        public string SignUpPolicyId { get; set; }

        public string DefaultPolicy => SignUpSignInPolicyId;

        public string ApiScopes { get; set; }
        public string ApiUrl { get; set; }
    }
}
