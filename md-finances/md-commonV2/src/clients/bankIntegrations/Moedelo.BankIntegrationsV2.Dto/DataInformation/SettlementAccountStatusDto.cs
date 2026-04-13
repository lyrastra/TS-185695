using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class SettlementAccountStatusDto
    {
        public string SettlementNumber { get; set; }

        public string Bik { get; set; }

        public IntegrationPartners IntegrationPartner { get; set; }

        public SettlementIntegrationStatus Status { get; set; }

        public bool RequestExtractAvailable { get; set; }
    }
}