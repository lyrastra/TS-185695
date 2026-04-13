using System;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class BankStatementRequestBySettlementAccounts
    {
        public BankStatementRequestBySettlementAccounts(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        /// <summary>
        /// Не запрашивать выписку, если есть необработанные запросы в банк 
        /// </summary>
        public bool StopOnUnprocessedRequest { get; set; }
    }
}