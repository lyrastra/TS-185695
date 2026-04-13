using Moedelo.AccountingV2.Dto.FinancialResults;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.FinancialResults
{
    public interface IAccountingBalanceApiClient : IDI
    {
        Task<FinancialResultsDto> GetFinancialResultsAsync(int firmId, int userId, int wizardStateId);

        Task<decimal> GetAdvanceStatementDebtsByWorkerAsync(int firmId, int userId, int workerId);
    }
}
