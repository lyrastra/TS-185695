using Moedelo.Common.Enums.Enums.Finances.Money;
using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Reconciliation
{
    public class ReconciliationResult
    {
        public ReconciliationResult()
        {
            ExcessOperations = new List<ReconciliationOperation>();
            MissingOperations = new List<ReconciliationOperation>();
        }

        public Guid SessionId { get; set; }

        public DateTime ReconcilationDate { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary> Есть в сервисе, нет в выписке </summary>
        public List<ReconciliationOperation> ExcessOperations { get; set; }

        /// <summary> Есть в выписке, нет в сервисе </summary>
        public List<ReconciliationOperation> MissingOperations { get; set; }

        public ReconciliationStatus Status { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
