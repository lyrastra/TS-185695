using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.RptV2.Dto.ReconciliationStatements;

using Moedelo.RptV2.Dto;

namespace Moedelo.RptV2.Client.ReconciliationStatements
{
    public interface IReconciliationStatementsApiClient : IDI
    {
        Task<List<ReconciliationStatementReportDto>> CreateAsync(int firmId, int userId, ReconciliationStatementQueryDto queryDto);
        Task<List<ReconcilationStatementFileDto>> GetReportFilesByKontragentIdsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);
        Task<decimal> GetKontragentDebtAsync(int firmId, int userId, int kontragentId, HttpQuerySetting setting = null);
    }
}
