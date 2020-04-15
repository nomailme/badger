using Badger.Web.Processors.CertificateQuery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Badger.Web.Processors.CertificateQuery.Extensions
{
    /// <summary>
    /// Extensions.
    /// </summary>
    public static class X509Certificate2Extensions
    {
        private const string SubjectAltNameOid = "2.5.29.17";

        /// <summary>
        /// Retrieve all records from Subject Alternative Name extension.
        /// </summary>
        /// <param name="certificate">Certificate.</param>
        /// <returns>List of tuples that include type and value of the record.</returns>
        public static List<(string Type, string Value)> GetSubjectAlternativeNames(this X509Certificate2 certificate)
        {
            var extension = certificate.Extensions
                .OfType<X509Extension>()
                .SingleOrDefault(x =>
                    string.Equals(x.Oid.Value, SubjectAltNameOid, StringComparison.InvariantCultureIgnoreCase));

            if (extension == null)
            {
                return new List<(string, string)>();
            }

            return extension.Format(true)
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(GetExtensionRecord)
                .ToList();
        }

        public static AddressCertificateInfo GetCertificateInfo(this X509Certificate2 certificate)
        {
            var result = new AddressCertificateInfo
            {
                Subject = certificate.GetNameInfo(X509NameType.SimpleName, false),
                Issuer = certificate.GetNameInfo(X509NameType.SimpleName,true),
                NotBefore = certificate.NotBefore,
                NotAfter = certificate.NotAfter,
                SubjectAltNames = certificate.GetSubjectAlternativeNames().Select(x => $"{x.Type}={x.Value}")
                    .ToList(),
            };
            return result;
        }

        private static (string Type, string Value) GetExtensionRecord(string input)
        {
            string[] entries = input.Split("=", 2, StringSplitOptions.RemoveEmptyEntries);
            var type = entries.First();
            var value = entries.Last();
            return (type, value);
        }
    }
}
