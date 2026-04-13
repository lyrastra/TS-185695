using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Funds;

namespace Moedelo.RequisitesV2.Client.Funds
{
    public interface IRosstatClient : IDI
    {
        Task<RosstatDepartmentDto> GetDepartmentAsync(int firmId, int userId);

        Task SaveDepartmentAsync(int firmId, int userId, RosstatDepartmentDto department);
    }
}