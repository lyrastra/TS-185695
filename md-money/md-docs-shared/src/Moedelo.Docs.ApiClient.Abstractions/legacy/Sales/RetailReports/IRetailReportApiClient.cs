using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.RetailReports.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.RetailReports
{
    /// <summary>
    /// https://github.com/moedelo/md-commonV2/blob/aff77181b55ed315336a763307a575f74f0850df/src/clients/accounting/Moedelo.AccountingV2.Client/RetailReport/IRetailReportClient.cs
    /// </summary>
    public interface IRetailReportApiClient
    {
        Task<List<RetailReportDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
        
        Task<RetailReportDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long baseId);
    }
}