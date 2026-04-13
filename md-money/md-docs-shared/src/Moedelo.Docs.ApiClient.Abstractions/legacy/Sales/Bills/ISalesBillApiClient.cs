using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Bills
{
    public interface ISalesBillApiClient
    {
        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
    }
}
