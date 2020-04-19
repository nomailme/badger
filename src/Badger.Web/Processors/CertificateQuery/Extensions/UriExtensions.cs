using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Badger.Web.Processors.CertificateQuery.Extensions
{
    public static class UriExtensions
    {
        public static X509Certificate2 GetCertificateAsync(this Uri uri)
        {
            var handler = new HttpClientHandler();
            X509Certificate2 serverCertificate = null;
            handler.ServerCertificateCustomValidationCallback = (message, certificate, chain, policyErrors) =>
            {
                serverCertificate = new X509Certificate2(certificate);
                return true;
            };

            var httpClient = new HttpClient(handler);
            var response = httpClient.GetAsync(uri).Result;
            if (serverCertificate == null)
            {
                throw new ArgumentException($"Unable to get certificate from {uri}");
            }
            return serverCertificate;
        }
    }
}
