using System;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.Reconciliation
{
    public class ReconciliationStatusRequestDto
    {
        public int[] SettlementAccountIds { get; set; }
        public DateTime? ReconciliationDate { get; set; }
    }
}
