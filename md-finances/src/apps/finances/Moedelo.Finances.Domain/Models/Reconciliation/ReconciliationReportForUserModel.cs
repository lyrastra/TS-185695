using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Reconciliation
{
    public class ReconciliationReportForUserModel
    {
        /// <summary>
        /// Идентификатор результатов сверки
        /// </summary>
        public Guid SessionId { get; set; }
        
        /// <summary>
        /// Идентификаторы операций, которые не нужно включать в формируемый отчёт
        /// </summary>
        public IReadOnlyCollection<long> ExcludeOperationsIds { get; set; }
    }
}