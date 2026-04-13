using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Municipal;

namespace Moedelo.CatalogV2.Client.Municipal
{
    public interface IMunicipalApiClient
    {
        Task<MunicipalDto> GetByIdAsync(int id);

        Task<MunicipalDto> GetByOktmoAsync(string oktmo, CancellationToken ctx  = default);

        Task<List<MunicipalDto>> GetByIfnsAsync(string ifns);

        Task<List<MunicipalDto>> GetByIfnsListAsync(IReadOnlyCollection<string> ifnsList);
    }
}