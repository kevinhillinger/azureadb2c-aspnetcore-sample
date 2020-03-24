
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApp.Security
{
    public static class IServiceCollectionExtensions
    {
        public class AzureAuthenticationConfig
        {
            public const string ConfigurationSectionName = "AzureAuthentication";

            public string Tenant { get; set; }
            public string ClientId { get; set; }

            public string Policy { get; set; }
            public List<string> Scopes { get; set; }

            public string Authority { get { return $"https://login.microsoftonline.com/tfp/{Tenant}/{ClientId}/v2.0/"; } }
            
            public string Audience { get { return ClientId; } }
        }
        
        public static void AddAzureAuthentication(IServiceCollection services, IConfiguration configuration) 
        {
            var s = services.BuildServiceProvider();
            s.Get
            var config = configuration.GetSection(AzureAuthenticationConfig.ConfigurationSectionName).Get<AzureAuthenticationConfig>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = config.Authority;
                options.Audience = config.Audience;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = AuthenticationFailed
                };
            });
        }

        private static Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg.Exception.Message}";
            arg.Response.ContentLength = s.Length;
            arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
            return Task.FromResult(0);
        }
    }
}