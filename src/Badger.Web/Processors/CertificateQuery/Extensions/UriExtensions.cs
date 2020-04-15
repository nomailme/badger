using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Badger.Web.Processors.CertificateQuery.Extensions
{
    public static class UriExtensions
    {
        public static async Task<X509Certificate2> GetCertificateAsync(this Uri uri)
        {
            var client = new TcpClient(uri.Host, uri.Port);
            var sslStream = new SslStream(client.GetStream());
            await sslStream.AuthenticateAsClientAsync(new SslClientAuthenticationOptions
            {
                TargetHost = uri.ToString(),
                RemoteCertificateValidationCallback = (sender, x509Certificate, chain, errors) => true
            });

            var certificate = new X509Certificate2(sslStream.RemoteCertificate);
            return certificate;
        }
    }
}
