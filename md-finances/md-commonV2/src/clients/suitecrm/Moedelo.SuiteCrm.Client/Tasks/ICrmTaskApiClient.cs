using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SuiteCrm.Dto.Tasks;

namespace Moedelo.SuiteCrm.Client.Tasks
{
    public interface ICrmTaskApiClient : IDI
    {
        Task<bool> CreateSimpleTaskAsync(SimpleTaskDto taskDto);
        Task<bool> CreateTaskWithSelectedOwnerAsync(SimpleTaskWithFreeOwnerDto taskDto);
        Task<bool> CreateSpendBonusesTask(SpendBonusesTaskDto taskDto);
        Task<bool> CreateClosingDocumentsTaskAsync(ClosingDocumentsTaskDto taskDto);
        Task<bool> CreateOutsourceRequestTaskAsync(OutsourceRequestTaskDto taskDto);
        Task<bool> CreateReactivationTaskAsync(CreateReactivationTaskDto taskDto);
        Task<bool> CreateCustomCommonEventTaskAsync(CustomCommonEventTaskDto taskDto);
    }
}