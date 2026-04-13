using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.DataInformation
{
    public class IntSummaryBySettlementsRequestDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public List<SettlementAccountV2Dto> Settlements { get; set; }
    }
}