using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Catalog.ApiClient.Abstractions.Regions.Dto;

namespace Moedelo.Catalog.ApiClient.Abstractions.Regions
{
    public interface IRegionApiClient
    {
        Task<RegionDto> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

        Task<RegionDto> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<RegionDto>> GetByIdsAsync(
            IReadOnlyCollection<int> ids,
            CancellationToken cancellationToken = default);

        Task<RegionDto> GetByPhoneAsync(
            string phone,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyDictionary<string, RegionDto>> GetByPhonesAsync(
            IReadOnlyCollection<string> phones,
            CancellationToken cancellationToken = default);
    }
}