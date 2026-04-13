using System;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class BankStatementRequestBySettlementAccount
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int SettlementAccountId { get; set; }
    }
}