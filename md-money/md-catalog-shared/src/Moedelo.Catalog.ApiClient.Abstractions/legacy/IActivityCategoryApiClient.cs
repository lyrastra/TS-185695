using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IActivityCategoryApiClient
    {
        Task<IReadOnlyCollection<ActivityCategoryDto>> GetByCodesAsync(IReadOnlyCollection<string> codes);

        Task<IReadOnlyCollection<ActivityCategoryDto>> GetByIdsAsync(IReadOnlyCollection<int> ids,
            CancellationToken cancellationToken = default);
    }
}