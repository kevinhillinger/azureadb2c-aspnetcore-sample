using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.Options;

namespace SampleWebApp.Security
{
    public class ClientCertificateHandler
    {
        private readonly CertificatesConfig certificatesConfig;
        
        public ClientCertificateHandler(IOptions<CertificatesConfig> certificatesConfig)
        {
            this.certificatesConfig = certificatesConfig.Value;
        }

        public Task Handle(CertificateValidatedContext context, ClientCertificate clientCertificate)
        {
            if (IsValidCertificate(clientCertificate))
            {
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, context.ClientCertificate.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                    new Claim(ClaimTypes.Name, context.ClientCertificate.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer)
                };
                context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                context.Success();
            } 
            else 
            { 
                context.Fail("Invalid Client Certificate"); 
            }
            
            return Task.CompletedTask;
        }

        private bool IsValidCertificate(ClientCertificate clientCertificate)
        {
             if (clientCertificate.IsNull)
            {
                return false;
            }

            var isValid = certificatesConfig.Any(c => clientCertificate.IsActive() 
                && clientCertificate.HasSubject(c.Subject)
                && clientCertificate.HasIssuer(c.Issuer)
                && clientCertificate.HasThumbprint(c.Thumbprint));
            
            return isValid;
        }
    }
}