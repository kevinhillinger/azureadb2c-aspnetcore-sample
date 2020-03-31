using System.Collections.Generic;

namespace SampleWebApp.Security
{
    public class CorsPolicyConfig
    {
        public string Name { get; set; } 
        public List<string> AllowedOrigins { get; set; }
    }
}