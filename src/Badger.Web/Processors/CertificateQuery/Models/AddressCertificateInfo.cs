using System;
using System.Collections.Generic;

namespace Badger.Web.Processors.CertificateQuery.Models
{
    /// <summary>
    /// Response.
    /// </summary>
    public class AddressCertificateInfo
    {
        /// <summary>
        /// Error information.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Certificate issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// End of the certificate validity period.
        /// </summary>
        public DateTime? NotAfter { get; set; }

        /// <summary>
        /// Certificate validity starting period.
        /// </summary>
        public DateTime? NotBefore { get; set; }

        /// <summary>
        /// Certificate subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Subject Alternative Name extension records.
        /// </summary>
        public List<string> SubjectAltNames { get; set; }

        /// <summary>
        /// Uri.
        /// </summary>
        public Uri Uri { get; set; }
    }
}
