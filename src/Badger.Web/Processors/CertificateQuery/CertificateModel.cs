using System;

namespace Badger.Web.Processors.CertificateQuery
{
    public class CertificateModel
    {
        private Uri uri;

        public CertificateModel(Uri uri)
        {
            this.uri = uri;
        }

        public DateTime NotBefore { get; set; } = DateTime.Now.AddDays(-1);

        public DateTime NotAfter { get; set; } = DateTime.Now.AddDays(1);

        public string CommonName { get; set; } = "CommomomomomName";

        public string Issuer { get; set; } = "Issuer";

    }
}
