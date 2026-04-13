using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Finances.Public.ClientData.Money.Reconciliation
{
    public class ReconciliationResultClientData
    {
        public Guid SessionId { get; set; }

        public IReadOnlyCollection<ReconciliationOperationClientData> ExcessOperations { get; set; } = Array.Empty<ReconciliationOperationClientData>();

        public IReadOnlyCollection<ReconciliationOperationClientData> MissingOperations { get; set; } = Array.Empty<ReconciliationOperationClientData>();

        public bool HasDiff => ExcessOperations.Any() || MissingOperations.Any();

        public ReconciliationStatus Status { get; set; }

        public DateTime InitialDate { get; set; }
        
        public Currency Currency { get; set; }

        public SettlementAccountClientData SettlementAccount { get; set; }
    }
}