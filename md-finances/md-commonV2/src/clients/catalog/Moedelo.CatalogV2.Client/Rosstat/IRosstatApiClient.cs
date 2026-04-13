using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Rosstat;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Rosstat
{
    public interface IRosstatApiClient : IDI
    {
        Task<RosstatDepartmentDto> GetDepartmentByOktmoAsync(string oktmo);

        Task<List<RosstatDepartmentDto>> GetDepartmentListByRegionCodeAsync(string regionCode);
    }
}