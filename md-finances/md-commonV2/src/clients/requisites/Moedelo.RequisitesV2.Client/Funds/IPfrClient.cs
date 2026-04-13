using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Funds;

namespace Moedelo.RequisitesV2.Client.Funds
{
    public interface IPfrClient : IDI
    {
        Task<PfrDepartmentDto> GetDepartmentAsync(int firmId, int userId);

        Task SaveDepartmentAsync(int firmId, int userId, PfrDepartmentDto department);

        Task<PfrDto> GetPfrByIdAsync(int id);

        Task<List<PfrDto>> GetPFRListByRegionAsync(int regionId);

    }
}