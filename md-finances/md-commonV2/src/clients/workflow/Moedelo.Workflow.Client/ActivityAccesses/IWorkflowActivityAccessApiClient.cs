using Moedelo.Workflow.Dto.ActivityAccesses;
using System.Threading.Tasks;

namespace Moedelo.Workflow.Client.ActivityAccesses;

public interface IWorkflowActivityAccessApiClient
{
    Task<ActivityAccessDto[]> CreateAsync(int accountId, string[] activityKeys);
}