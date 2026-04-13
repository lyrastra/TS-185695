using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.MiddlemanReports
{
    public interface IMiddlemanReportClient
    {
        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
    }
}