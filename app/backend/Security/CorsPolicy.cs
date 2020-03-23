using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleWebApp.Security
{
    class CorsPolicyConfig
    {
        public const string ConfigurationSectionName = "CorsPolicies";

        public string Name { get; } 
        public string[] Origins { get; set; }
    }

    static class CorsPolicyExtensions
    {
        public static void AddCorsUsingConfiguration(this IServiceCollection services, IConfiguration configuration) {
            var policies = new List<CorsPolicyConfig>();
            configuration.GetSection(CorsPolicyConfig.ConfigurationSectionName).Bind(policies);
            services.AddCors(options => policies.ForEach(options.AddPolicy));
        }
        
        public static void AddPolicy(this CorsOptions options, CorsPolicyConfig policy)
        {
            options.AddPolicy(policy.Name, builder => {
                builder.WithOrigins(policy.Origins);
            });
        }
    }
}