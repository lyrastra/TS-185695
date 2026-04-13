using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor
{
    /// <summary>
    /// Используемый банком сертификат из консула
    /// </summary>
    public class CertificateThumbprintDto
    {
        /// <summary>
        /// Банк-партнер
        /// </summary>
        public IntegrationPartners IntegrationPartner { get; set; }
        /// <summary>
        /// Банк использует сертификат
        /// </summary>
        public bool UseCertificate { get; set; }
        /// <summary>
        /// Отпечаток используемого сертификата из консула
        /// </summary>
        public string Thumbprint { get; set; }
    }
}