using System.Collections.Generic;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IFnsApiClient
    {
        Task<FnsRequisitesDto> GetRequisitesByCodeAndOktmoAsync(string code, string oktmo);

        Task<FnsDto> GetByCodeAsync(string code);

        Task<List<FnsDto>> GetByRegionAsync(string regionCode);

        Task<List<FnsDto>> GetByCodesAsync(IReadOnlyCollection<string> codes);
    }
}
