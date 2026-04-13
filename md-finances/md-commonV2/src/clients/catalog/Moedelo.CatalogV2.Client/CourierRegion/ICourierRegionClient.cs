using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.CourierRegion;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.CourierRegion
{
    public interface ICourierRegionClient : IDI
    {
        Task<List<CourierRegionDto>> GetAllAsync();
        Task<int> SaveAsync(CourierRegionDto dto);
        Task DeleteAsync(int id);
        Task<string> GetCourierRegionCodeByFirmCodeAsync(string code);
        Task<List<string>> GetAllForAutocompleteAsync();
    }
}