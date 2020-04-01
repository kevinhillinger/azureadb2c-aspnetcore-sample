using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.DependencyInjection;

namespace SampleWebApp.Security
{
    static class CertificatesExtensions
    {
        public static void AddCertificateAuthentication(this IServiceCollection services) 
        {
            services.AddTransient<ClientCertificateHandler>();

             services
                .AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate(options => 
                {
                    options.AllowedCertificateTypes = CertificateTypes.SelfSigned;
                    
                    options.Events = new CertificateAuthenticationEvents
                    {
                        OnCertificateValidated = context => 
                        {
                            var clientCertificate = context.GetClientCertificate();
                            var handler = context.HttpContext.RequestServices.GetService<ClientCertificateHandler>();
          
                            return handler.Handle(context, clientCertificate);
                        }
                    };
                });

            services.AddAuthorization();

            services.AddCertificateForwarding(options =>
            {
                options.CertificateHeader = "X-ARR-ClientCert";
                options.HeaderConverter = (headerValue) =>
                {
                    X509Certificate2 clientCertificate = null;
                    if(!string.IsNullOrWhiteSpace(headerValue))
                    {
                        byte[] bytes = Convert.FromBase64String(headerValue);
                        clientCertificate = new X509Certificate2(bytes);
                    }
                    return clientCertificate;
                };
            });
        }

        private static ClientCertificate GetClientCertificate(this CertificateValidatedContext context)
        {
            return new ClientCertificate(context.ClientCertificate);
        }
    }
}