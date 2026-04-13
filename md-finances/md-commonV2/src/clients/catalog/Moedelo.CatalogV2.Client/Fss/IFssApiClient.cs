using Moedelo.CatalogV2.Dto.Fss;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.CatalogV2.Client.Fss
{
    public interface IFssApiClient : IDI
    {
        Task<List<FssDto>> GetByRegionAsync(string regionCode);

        Task<FssDto> GetByCodeAsync(string code);
    }
}
