using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Fns;

namespace Moedelo.CatalogV2.Client.Fns
{
    public interface IFnsApiClient
    {
        Task<FnsDto> GetByIdAsync(int id, CancellationToken ct = default);

        Task<FnsDto> GetByCodeAsync(string code);

        Task<List<FnsDto>> GetByCodesAsync(IReadOnlyCollection<string> codes);

        Task<List<FnsDto>> GetByRegionAsync(string regionCode, CancellationToken ct = default);

        Task<FnsWithRequisitesDto> GetWithRequisitesAsync(int id, string oktmo);

        Task<FnsWithRequisitesDto> GetWithRequisitesByCodeAndOktmoAsync(string code, string oktmo);

        Task<FnsRequisitesDto> GetRequisitesByIdAndOktmoAsync(int id, string oktmo);

        Task<FnsRequisitesDto> GetRequisitesByCodeAndOktmoAsync(string code, string oktmo);
    }
}