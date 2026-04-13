using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class GetActiveIntegrationsForFirmResponse
    {
        public List<IntegrationPartners> Data { get; set; }
    }
}
