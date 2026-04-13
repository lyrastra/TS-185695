using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Moedelo.CommonV2.Auth.OAuth2.Services
{
    internal static class X509Loader
    {
        internal static RSA LoadPrivateKey(string serial)
        {
            var cert = LoadMoedeloCertificate(serial);

            return cert?.GetRSAPrivateKey()
                   ?? throw new Exception($"Не удалось найти приватный RSA ключ по номеру {serial}");
        }
        
        private static X509Certificate2 LoadMoedeloCertificate(string certificateSerial)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                var result = FindCertificate(store, certificateSerial);
                return result;
            }
            finally
            {
                store.Close();
            }
        }

        private static X509Certificate2 FindCertificate(X509Store store, string certificateSerial)
        {
            store.Open(OpenFlags.ReadOnly);
            var result = store.Certificates.Cast<X509Certificate2>()
                .FirstOrDefault(
                    cert => cert.SerialNumber != null
                            && cert.SerialNumber.Equals(certificateSerial, StringComparison.InvariantCultureIgnoreCase));
            return result;
        }
    }
}