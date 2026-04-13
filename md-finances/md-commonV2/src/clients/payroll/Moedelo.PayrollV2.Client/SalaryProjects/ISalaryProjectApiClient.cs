using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.PayrollV2.Client.SalaryProjects
{
    public interface ISalaryProjectApiClient : IDI
    {
        Task<List<int>> GetSalaryProjectSettlementAccountIds(int firmId, int userId);
        
        Task UnbindRegistryPaymentAsync(int firmId, int userId, long documentBaseId);
    }
}
