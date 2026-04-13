using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Client.Money.Dto
{
    public class LastReconciliationWithDiffRequestDto
    {
        public ReconciliationStatus ReconciliationStatus { get; set; }
        public IReadOnlyCollection<int> SettlementAccountIds { get; set; }
    }
}
