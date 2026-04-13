using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Region;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Region
{
    public interface IRegionApiClient : IDI
    {
        Task<List<RegionDto>> GetAllAsync();

        Task<RegionDto> GetByIdAsync(int id);
        
        Task<List<RegionDto>> GetByIdsAsync(IReadOnlyCollection<int> regionIds);

        Task<RegionDto> GetByCodeAsync(string code);
        
        Task<RegionDto> GetByPhoneAsync(string phone);

        Task<int?> GetRegionIdByIPAddressAsync(string ipAddress);
    }
}