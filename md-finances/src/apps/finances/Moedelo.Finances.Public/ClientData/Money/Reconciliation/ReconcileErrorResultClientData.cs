using System;
using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Public.ClientData.Money.Reconciliation
{
    public class ReconcileErrorResultClientData
    {
        public Guid SessionId { get; set; }

        public ReconciliationStatus Status { get; set; }
    }
}