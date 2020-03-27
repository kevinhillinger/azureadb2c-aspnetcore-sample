using System.Collections.Generic;

namespace SampleWebApp.Security
{
    public class CertificatesConfig : List<ClientCertificateConfig>
    {
        public const string ConfigurationSectionName = "Certificates";
    }
}