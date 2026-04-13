using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SuiteCrm.Dto.Bpm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.SuiteCrm.Client
{
    public interface IEmployeesApiClient : IDI
    {
        Task<EmployeeDto[]> GetByIdsAsync(int accountId, IReadOnlyCollection<int> ids);
    }
}
