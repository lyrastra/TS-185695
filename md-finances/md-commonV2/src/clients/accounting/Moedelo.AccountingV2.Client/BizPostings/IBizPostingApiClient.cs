using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.BizPostings
{
    public interface IBizPostingApiClient : IDI
    {
        Task<List<BizPostingDto>> GetBizPostingsForReconciliationStatementAsync(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<List<BizPostingReportDto>> GetBizPostingReportAsync(int firmId, int userId, DateTime startDate, DateTime endDate, bool isReconciliationStatement);
    }
}
