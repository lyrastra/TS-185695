using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.Dto
{
    public class BaseRequestDto
    {
        public string Inn { get; set; }
        public string Bik { get; set; }
        public string Login { get; set; }
        public int FirmId { get; set; }
        public string SettlementNumber { get; set; }
        public IntegrationPartners Partner { get; set; }
        public string ExternalClientId { get; set; }
    }
}
