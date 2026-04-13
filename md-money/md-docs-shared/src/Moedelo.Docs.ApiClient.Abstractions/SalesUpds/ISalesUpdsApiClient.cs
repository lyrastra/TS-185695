using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUpds
{
    public interface ISalesUpdsApiClient
    {
        Task<long> GetNextNumberAsync(int firmId, int userId, int year);

        Task<bool> IsDocumentNumberBusyAsync(int firmId, int userId, int year, string number);

        Task<SalesUpdRestDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task<List<SalesUpdDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds,
            CancellationToken cancellationToken);

        Task<List<SalesUpdWithItemsDto>> GetWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}