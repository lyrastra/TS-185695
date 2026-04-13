using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.ReconciliationStatements;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccountingV2.Client.ReconciliationStatements
{
    public interface IReconciliationStatementsApiClient : IDI
    {
        Task<List<ReconciliationStatementReportDto>> CreateAsync(int firmId, int userId, ReconciliationStatementQueryDto queryDto);

        Task<decimal> GetKontragentDebtAsync(int firmId, int userId, int kontragentId, HttpQuerySetting setting = null);
    }
}
