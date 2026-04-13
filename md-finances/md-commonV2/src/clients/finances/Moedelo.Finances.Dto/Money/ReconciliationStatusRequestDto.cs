using System;

namespace Moedelo.Finances.Dto.Money
{
    public class ReconciliationStatusRequestDto
    {
        public int[] SettlementAccountIds { get; set; }
        public DateTime? ReconciliationDate { get; set; }
    }
}
