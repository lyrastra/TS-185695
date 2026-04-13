using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.PonyExpress;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.PonyExpress
{
    public interface IPonyExpressOfficeApiClient : IDI
    {
        Task<List<PonyExpressOfficeDto>> GetListByRegionIdAsync(int regionId);
        Task<List<PonyExpressOfficeDto>> GetListAsync();
        Task<byte[]> GetFileAsync();
    }
}