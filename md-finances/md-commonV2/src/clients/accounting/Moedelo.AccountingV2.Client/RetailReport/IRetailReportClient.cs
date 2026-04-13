using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.RetailReport;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.RetailReport
{
    public interface IRetailReportApiClient : IDI
    {
        Task<SavedRetailReportDto> CreateAsync(int firmId, int userId, RetailReportDto dto);

        Task<List<RetailReportDto>> GetListV2Async(int firmId, int userId, RetailReportRequest request);

        Task<List<RetailReportDto>> GetListAsync(int firmId, int userId, RetailReportPaginationRequest request);

        Task<RetailReportDto> GetByBaseIdAsync(int firmId, int userId, long id);

        Task<List<RetailReportDto>> GetForPeriodAsync(int firmId, int userId, DateTime? afterDate= null,  DateTime? beforeDate = null);

        Task<List<RetailReportDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<SavedRetailReportDto> UpdateAsync(int firmId, int userId, RetailReportDto dto);

        Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId);
    }
}