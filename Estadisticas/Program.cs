using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Estadisticas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ConfigureHttpsDefaults(listenOptions =>
                        {
                            // certificate is an X509Certificate2
                            listenOptions.ServerCertificate = CreateSelfSignedCert("app-server", "elcapo1072", "certificate.pfx","Clinica Constituyentes");
                        });
                    })
                    .UseStartup<Startup>();
                });

        public static X509Certificate2 CreateSelfSignedCert(string CommonName, string Password, string Destination, string FriendlyName = null, string[] DnsNames = null, DateTime? ExpirationBefore = null, DateTime? ExpirationAfter = null, bool IsCertificateAuthority = false, string CountryCode = "US", string Organization = "JCCE", string[] OrganizationalUnits = null)
        {
            SubjectAlternativeNameBuilder sanBuilder = new SubjectAlternativeNameBuilder();
            if (DnsNames == null)
            {
                sanBuilder.AddIpAddress(IPAddress.Loopback);
                sanBuilder.AddIpAddress(IPAddress.IPv6Loopback);
                sanBuilder.AddDnsName("localhost");
                sanBuilder.AddDnsName(Environment.MachineName);
            }
            else
            {
                foreach (var dnsName in DnsNames)
                {
                    sanBuilder.AddDnsName(dnsName);
                }
            }

            if (CountryCode.Length != 2) CountryCode = "US";
            if (OrganizationalUnits == null) OrganizationalUnits = new[] { "Copyright (c), " + DateTime.UtcNow.ToString("yyyy") + " JCCE" };
            var dn = new StringBuilder();
            dn.Append("CN=\"" + CommonName.Replace("\"", "\"\"") + "\"");
            foreach (var ou in OrganizationalUnits)
            {
                dn.Append(",OU=\"" + ou.Replace("\"", "\"\"") + "\"");
            }
            dn.Append(",O=\"" + Organization.Replace("\"", "\"\"") + "\"");
            dn.Append(",C=" + CountryCode.ToUpper());
            dn.Append(",C=" + "JP");

            var strDn = dn.ToString();
            X500DistinguishedName distinguishedName = new X500DistinguishedName(strDn);
            X509Certificate2 cert;
            using (RSA rsa = RSA.Create(2048))
            {
                var request = new CertificateRequest(distinguishedName, rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                var usages = X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature;
                if (IsCertificateAuthority) usages = usages | X509KeyUsageFlags.KeyCertSign;
                request.CertificateExtensions.Add(new X509KeyUsageExtension(usages, false));
                request.CertificateExtensions.Add(
                   new X509EnhancedKeyUsageExtension(
                       new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, false));
                request.CertificateExtensions.Add(sanBuilder.Build());
                if (IsCertificateAuthority) request.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 1, true));
                if (ExpirationAfter == null) { ExpirationAfter = DateTime.UtcNow.AddDays(-1).AddYears(10); }
                if (ExpirationBefore == null) ExpirationBefore = DateTime.UtcNow;
                var certificate = request.CreateSelfSigned(new DateTimeOffset(ExpirationBefore.Value), new DateTimeOffset(ExpirationAfter.Value));
                if (FriendlyName == null) FriendlyName = CommonName;
                //certificate.FriendlyName = FriendlyName;
                cert = new X509Certificate2(certificate.Export(X509ContentType.Pfx, Password), Password, X509KeyStorageFlags.MachineKeySet);
                File.WriteAllBytes(Destination, certificate.Export(X509ContentType.Pkcs12, Password));
            }
            return cert;
        }
    }
}
