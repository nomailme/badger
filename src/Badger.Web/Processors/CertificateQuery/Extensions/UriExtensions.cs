using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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
                policyErrors = System.Net.Security.SslPolicyErrors.None;
                serverCertificate = certificate;
                return true;
            };

            var httpClient = new HttpClient();
            try
            {
                var response = httpClient.GetAsync(uri).Result;

            }
            catch(AuthenticationException)
            {
            }
            //var client = new TcpClient(uri.Host, uri.Port);

            //var sslStream = new SslStream(client.GetStream());
            //await sslStream.AuthenticateAsClientAsync(new SslClientAuthenticationOptions
            //{
            //    TargetHost = uri.ToString(),
            //    RemoteCertificateValidationCallback = (sender, x509Certificate, chain, errors) => true
            //});

            //var certificate = new X509Certificate2(sslStream.RemoteCertificate);
            return serverCertificate;
        }
    }
}
