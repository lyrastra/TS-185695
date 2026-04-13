using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.Dto.Movements
{
    public class GetMovementStatusRequestDto
    {
        public string RequestId { get; set; }

        public string SettlementNumber { get; set; }

        public string Bik { get; set; }

        public int FirmId { get; set; }

        public IntegrationPartners Partner { get; set; }

        public string Login { get; set; }
    }
}
