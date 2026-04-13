using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation
{
    public class RequestMovementForAllSettlementsDto
    {
        public int FirmId { get; set; }

        public IntegrationPartners Partner { get; set; }
    }
}
