using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class IntegrationPartnerRequestsCountDto
    {
        public IntegrationPartners IntegrationPartner { get; set; }
        public int Count { get; set; }
    }
}
