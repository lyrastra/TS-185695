using System.Collections.Generic;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IFssApiClient
    {
        Task<FssDto> GetByCodeAsync(string code);
        Task<List<FssDto>> GetByRegionAsync(string regionCode);
    }
}
