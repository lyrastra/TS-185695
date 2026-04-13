using System.Threading;
using System.Threading.Tasks;
using Moedelo.Workflow.Dto.Users;

namespace Moedelo.Workflow.Client.Users;

public interface IWorkflowUserApiClient
{
    Task<WorkflowUserDto> GetByUserIdAsync(int firmId, int userId, CancellationToken cancellationToken = default);

    Task<WorkflowUserDto> CreateAsync(WorkflowUserDto model);
}