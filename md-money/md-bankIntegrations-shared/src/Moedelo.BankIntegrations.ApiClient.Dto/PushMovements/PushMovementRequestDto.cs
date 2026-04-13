using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.PushMovements
{
    public class PushMovementRequestDto
    {
        public IntegrationPartners PartnerId { get; set; }

        public string RequestGuid { get; set; }

        public string KafkaKey { get; set; }

        public string Status { get; set; }

        public string ErrorMessage { get; set; }

        public string MovementsZip { get; set; }

        public string Encoding { get; set; }
    }
}
