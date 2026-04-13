using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Funds;

namespace Moedelo.RequisitesV2.Client.Funds
{
    public interface IFnsClient : IDI
    {
        Task<FnsDepartmentDto> GetDepartmentAsync(int firmId, int userId);

        Task SaveDepartmentAsync(int firmId, int userId, FnsDepartmentDto department);

        Task<FnsDepartmentDto> GetUnifiedBudgetaryPaymentDepartmentInfoAsync(int firmId, int userId);
    }
}