using System;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation
{
    public interface IReconciliationComparer : IDI
    {
        Task<ReconciliationCompareResult> CompareWithBankStatementAsync(IUserContext userContext, string fileText, DateTime startDate, DateTime endDate);

        Task<ReconciliationCompareResult> CompareWithEmptyBankStatementAsync(IUserContext userContext, string settlementAccountNumber, DateTime startDate, DateTime endDate);
    }
}
