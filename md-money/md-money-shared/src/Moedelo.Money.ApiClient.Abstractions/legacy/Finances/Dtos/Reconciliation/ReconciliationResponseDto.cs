using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.Reconciliation
{
    public class ReconciliationResponseDto
    {
        public Guid SessionId { get; set; }
        public int SettlementAccountId { get; set; }
        public DateTime CreateDate { get; set; }
        public ReconciliationStatus ReconciliationStatus { get; set; }
    }
}
