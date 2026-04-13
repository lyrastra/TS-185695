using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class IntegrationPartnerRequestsInStatusCountDto
    {
        public IntegrationPartners IntegrationPartner { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public int Count { get; set; }
    }
}
