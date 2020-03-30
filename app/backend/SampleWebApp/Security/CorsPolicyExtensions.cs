using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SampleWebApp.Security
{
    static class CorsPolicyExtensions
    {
        public static void AddCorsPolicies(this IServiceCollection services) {
            var policies = services.BuildServiceProvider().GetService<IOptions<List<CorsPolicyConfig>>>().Value;
            services.AddCors(options => policies.ForEach(options.AddPolicy));
        }
        
        public static void AddPolicy(this CorsOptions options, CorsPolicyConfig policy)
        {
            options.AddPolicy(policy.Name, builder => {
                builder.WithOrigins(policy.AllowedOrigins.ToArray());
            });
        }
    }
}