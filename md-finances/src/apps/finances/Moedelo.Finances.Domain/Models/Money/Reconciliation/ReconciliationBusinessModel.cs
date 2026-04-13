using Moedelo.Finances.Domain.SettlementAccounts;

namespace Moedelo.Finances.Domain.Models.Money.Reconciliation
{
    public class ReconciliationBusinessModel
    {
        public ReconciliationResult ReconciliationResult { get; set; }
        
        public SettlementAccount SettlementAccount { get; set; }
    }
}