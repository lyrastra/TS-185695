using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class UpdateIntegrationDataRequestDto
    {
        public IntegrationPartners Partner { get; set; }
        public int FirmId { get; set; }
        public string IntegrationData { get; set; }
    }
}