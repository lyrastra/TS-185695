using System.Collections.Generic;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.Integrations
{
    public class GetActiveIntegrationsForFirmResponse
    {
        public List<IntegrationPartners> Data { get; set; }
    }
}
