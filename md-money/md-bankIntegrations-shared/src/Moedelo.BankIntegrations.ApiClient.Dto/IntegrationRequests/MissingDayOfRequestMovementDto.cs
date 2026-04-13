using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests
{
    public class MissingDayOfRequestMovementDto
    {
        public int FirmId { get; set; }
        public string SettlementNumber { get; set; }
        public string Bik { get; set; }
        public string Inn { get; set; }
        public string MissingDate { get; set; }
    }
}
