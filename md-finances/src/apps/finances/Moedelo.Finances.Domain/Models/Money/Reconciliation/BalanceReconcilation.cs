using System;
using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Domain.Models.Money.Reconciliation
{
    public class BalanceReconcilation
    {
        public int Id { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal ServiceBalance { get; set; }

        public decimal BankBalance { get; set; }

        public DateTime ReconcilationDate { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid SessionId { get; set; }

        public ReconciliationStatus Status { get; set; }
    }
}
