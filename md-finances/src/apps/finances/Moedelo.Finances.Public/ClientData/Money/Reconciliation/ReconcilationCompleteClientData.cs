using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money.Reconciliation
{
    public class ReconcilationCompleteClientData
    {
        public List<long> ExcessOperations { get; set; }

        public List<long> MissingOperations { get; set; }

        public Guid SessionId { get; set; }
    }
}