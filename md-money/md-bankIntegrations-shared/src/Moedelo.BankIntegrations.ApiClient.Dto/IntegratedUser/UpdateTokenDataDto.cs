using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class UpdateTokenDataRequestDto
    {
        public IntegrationPartners Partner { get; set; }
        public int FirmId { get; set; }
        public string TokenData { get; set; }
    }
}