using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SampleWebApp.Security
{
    public class ClientCertificate
    {
         public static Func<DateTime> Now => () => DateTime.Now;

        public string Name { get; }

        public bool IsNull {get { return Certificate == null; } }

        private X509Certificate2 Certificate { get; }

        private IEnumerable<string> Subjects
        {
            get { return Certificate?.Subject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); }
        }

        private IEnumerable<string> Issuers
        {
            get { return Certificate?.Issuer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); }
        }

        public ClientCertificate(X509Certificate2 certificate)
        {
            Certificate = certificate;
        }

        public bool IsActive()
        {
            if (IsNull)
            {
                return false;
            }
            
            var now = Now();
            
            var isNowLessThanCertificateStart = DateTime.Compare(now, Certificate.NotBefore) < 0;
            var isNowGreaterThanCertificateExpiration = DateTime.Compare(now, Certificate.NotAfter) > 0;

            if (!isNowLessThanCertificateStart || !isNowGreaterThanCertificateExpiration)
            {
                return true;
            }
            return false;
        }

        internal bool HasIssuer(string issuer)
        {
            return Issuers.Any(s => string.Compare(s.Trim(), issuer) == 0);
        }

        public bool HasSubject(string subject)
        {
           return Subjects.Any(s => string.Compare(s.Trim(), subject) == 0);
        }

        public bool HasThumbprint(string thumbprint)
        {
            return string.Compare(Certificate?.Thumbprint.Trim().ToUpper(), thumbprint) == 0;
        }
    }
}