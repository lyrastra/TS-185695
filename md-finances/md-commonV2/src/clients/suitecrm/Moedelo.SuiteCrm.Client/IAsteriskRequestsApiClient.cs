using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.SuiteCrm.Client
{
    public interface IAsteriskRequestsApiClient : IDI
    {
        Task SendTaskToAsteriskAsync(string taskId);

        Task DeleteTaskFromAsteriskAsync(string taskId);
    }
}