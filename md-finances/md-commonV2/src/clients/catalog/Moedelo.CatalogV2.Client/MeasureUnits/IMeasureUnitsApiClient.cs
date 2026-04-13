using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.MeasureUnits;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.MeasureUnits
{
    public interface IMeasureUnitsApiClient : IDI
    {
        Task<List<MeasureUnitDto>> GetListAsync();
    }
}