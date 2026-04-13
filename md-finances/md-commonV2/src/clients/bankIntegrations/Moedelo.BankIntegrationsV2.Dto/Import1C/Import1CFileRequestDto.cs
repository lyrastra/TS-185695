using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.Import1C
{
    public class Import1CFileRequestDto
    {
        public string Text1C { get; set; }

        public string KafkaKey { get; set; }

        public IntegrationPartners IntegrationPartner { get; set; }

        public string ExternalRequestId { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }
    }
}
