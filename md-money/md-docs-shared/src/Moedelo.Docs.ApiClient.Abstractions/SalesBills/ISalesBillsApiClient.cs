using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SalesBills.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesBills
{
    public interface ISalesBillsApiClient
    {
        Task<List<SalesBillDto>> GetByBaseIdsAsync(IReadOnlyCollection<long> ids);

        Task<DataPageResponse<SalesBillByCriteriaResponseDto>> GetBillByCriteriaAsync(
            SalesBillByCriteriaRequestDto criteria);
    }
}