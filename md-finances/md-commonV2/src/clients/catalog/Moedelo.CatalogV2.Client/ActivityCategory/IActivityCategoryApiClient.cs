using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.ActivityCategory;

namespace Moedelo.CatalogV2.Client.ActivityCategory
{
    public interface IActivityCategoryApiClient
    {
        Task<ActivityCategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<List<ActivityCategoryDto>> GetByIdListAsync(IReadOnlyCollection<int> idList, CancellationToken cancellationToken = default);

        Task<ActivityCategoryDto> GetByCodeAndNameAsync(string code, string name, CancellationToken cancellationToken = default);

        Task<List<ActivityCategoryDto>> GetAutocompleteByCodeAsync(string code, int count = 5, CancellationToken cancellationToken = default);

        Task<List<ActivityCategoryDto>> GetAutocompleteByCodeOrNameAsync(string query, int count, CancellationToken cancellationToken);

        Task<List<ActivityCategoryDto>> GetByCodesAsync(IReadOnlyCollection<string> codeList, CancellationToken cancellationToken = default);
    }
}