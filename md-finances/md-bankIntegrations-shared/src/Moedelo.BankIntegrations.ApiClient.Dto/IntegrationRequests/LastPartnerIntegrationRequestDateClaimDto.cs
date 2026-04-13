using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class LastPartnerIntegrationRequestDateClaimDto
    {
        public int FirmId { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
        public IntegrationCallType[] Types { get; set; }
        public RequestStatus[] Statuses { get; set; }
    }
}
