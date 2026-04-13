using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport;

namespace Moedelo.Money.Reports.DataAccess.Abstractions.Reconciliation
{
    public interface IReconciliationDao
    {
        Task<IReadOnlyList<SettlementAccountReconciliation>> GetSettlementAccountReconciliationAsync(
            IReadOnlyCollection<int> firmIds);
    }
}
