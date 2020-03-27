using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SampleWebApp.Security
{
    static class CertificatesExtensions
    {
        public static void AddCertificateAuthentication(this IServiceCollection services) 
        {
            services.AddTransient<ClientCertificateHandler>();

          //  var provider = services.BuildServiceProvider();
          //  var certificatesConfig = provider.GetService<IOptions<CertificatesConfig>>().Value;

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
                        byte[] bytes = StringToByteArray(headerValue);
                        clientCertificate = new X509Certificate2(bytes);
                    }
                    return clientCertificate;
                };
            });

            // foreach (var clientCertificateConfig in certificatesConfig)
            // {
            //     var name = clientCertificateConfig.Key;

            //     services.AddAuthorization(options =>
            //     {
            //         options.AddPolicy(name = $"{name}{CertificateAuthenticationDefaults.AuthenticationScheme}Policy", policy =>
            //         {
            //             policy.AuthenticationSchemes.Add(CertificateAuthenticationDefaults.AuthenticationScheme);
            //         });
            //     });
            // }
        }

        private static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private static ClientCertificate GetClientCertificate(this CertificateValidatedContext context)
        {
            return new ClientCertificate(context.ClientCertificate);
        }
    }
}