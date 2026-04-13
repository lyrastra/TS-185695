using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.Models.IntegrationMonitor;

namespace Moedelo.BankIntegrations.Mapper.IntegrationMonitor
{
    public static class CertificateThumbprintMapper
    {
        public static CertificateThumbprintModel Map(this CertificateThumbprintDto dto)
        {
            return new CertificateThumbprintModel() 
            {
                IntegrationPartner = dto.IntegrationPartner,
                Thumbprint = dto.Thumbprint,
                UseCertificate = dto.UseCertificate
            };
        }

        public static CertificateThumbprintDto Map(this CertificateThumbprintModel model)
        {
            return new CertificateThumbprintDto()
            {
                IntegrationPartner = model.IntegrationPartner,
                Thumbprint = model.Thumbprint,
                UseCertificate = model.UseCertificate
            };
        }

    }
}
