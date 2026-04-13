using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class AcceptanceBlockDto
    {
        public int FirmId { get; set; }

        public int IntegrationPartner { get; set; }

        public DateTime? AcceptanceSettlementCheckLastDate { get; set; }

        public bool? BlockedAcceptanceSettlement { get; set; }

        public bool? ClosedAcceptanceSettlement { get; set; }
    }
}
