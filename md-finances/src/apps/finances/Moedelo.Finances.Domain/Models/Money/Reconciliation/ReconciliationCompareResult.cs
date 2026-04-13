using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Reconciliation
{
    public class ReconciliationCompareResult
    {
        /// <summary> Есть в сервисе, нет в выписке </summary>
        public List<ReconciliationOperation> ExcessOperations { get; set; }

        /// <summary> Есть в выписке, нет в сервисе </summary>
        public List<ReconciliationOperation> MissingOperations { get; set; }
    }
}
