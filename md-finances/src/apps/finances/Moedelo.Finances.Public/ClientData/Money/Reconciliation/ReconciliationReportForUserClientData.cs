using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money.Reconciliation
{
    public class ReconciliationReportForUserClientData
    {
        public Guid SessionId { get; set; }
        public IReadOnlyCollection<long> ExcludeOperationsIds { get; set; }
    }
}