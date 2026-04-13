using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class DisableIntegrationRequestDto
    {
        public int FirmId { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
        public bool ResetExternalId { get; set; }
    }
}
