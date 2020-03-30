using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SampleWebApp.Security
{
    public class CorsPolicyConfig
    {
        public const string ConfigurationSectionName = "CorsPolicies";

        public string Name { get; set; } 
        public List<string> AllowedOrigins { get; set; }
    }
}