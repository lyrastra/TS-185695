using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport
{
    public class SettlementAccountReconciliation
    {
        public int SettlementAccountId { get; set; }

        public DateTime CreateDate { get; set; }

        public ReconciliationStatus Status { get; set; }
    }
}
