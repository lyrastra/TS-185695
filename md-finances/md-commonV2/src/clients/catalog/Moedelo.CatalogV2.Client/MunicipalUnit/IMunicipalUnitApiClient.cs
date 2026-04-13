using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.MunicipalUnit;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.MunicipalUnit
{
    public interface IMunicipalUnitApiClient : IDI
    {
        Task<MunicipalUnitDto> GetByOktmoAsync(string oktmo);
    }
}