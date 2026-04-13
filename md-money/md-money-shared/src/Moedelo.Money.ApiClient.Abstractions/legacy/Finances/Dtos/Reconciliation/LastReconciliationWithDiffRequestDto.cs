using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.Reconciliation
{
    public class LastReconciliationWithDiffRequestDto
    {
        public ReconciliationStatus ReconciliationStatus { get; set; }
        public IReadOnlyCollection<int> SettlementAccountIds { get; set; }
    }
}
