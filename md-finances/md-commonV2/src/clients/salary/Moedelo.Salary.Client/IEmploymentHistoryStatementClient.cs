using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Salary.Dto;

namespace Moedelo.Salary.Client
{
    public interface IEmploymentHistoryStatementClient : IDI
    {
        Task<EmploymentHistoryStatementDto> GetByWorkerAsync(
            int firmId,
            int userId,
            int workerId);

        Task<int> InsertAsync(
            int firmId,
            int userId,
            EmploymentHistoryStatementDto model);

        Task UpdateAsync(
            int firmId,
            int userId,
            EmploymentHistoryStatementDto model);
    }
}