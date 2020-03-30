namespace SampleWebApp.Security
{
    public class ClientCertificateConfig
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Issuer { get; set; }
        public string Thumbprint { get; set; }
    }
}