using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Pfr;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Pfr
{
    public interface IPfrApiClient : IDI
    {
        Task<List<PfrDepartmentDto>> GetDepartmentListByRegionCodeAsync(string regionCode);

        Task<PfrDepartmentDto> GetDepartmentByCodeAsync(string code);

        Task<PfrRequisitesDto> GetRequisitesByRegionCodeAsync(string regionCode);

        Task<List<PfrDepartmentDto>> GetDepartmentsListByCodeTemplateAsync(string codeTemplate);
    }
}