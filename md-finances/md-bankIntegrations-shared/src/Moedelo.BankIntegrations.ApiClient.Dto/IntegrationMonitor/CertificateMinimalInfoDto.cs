using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    public class CertificateMinimalInfoDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string DnsName { get; set; }
        public string EmailName { get; set; }
        public string Issuer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Subject { get; set; }
        public string Thumbprint { get; set; }
    }
}