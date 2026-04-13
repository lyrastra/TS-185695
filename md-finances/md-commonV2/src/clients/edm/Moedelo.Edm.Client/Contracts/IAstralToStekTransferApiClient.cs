using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IAstralToStekTransferApiClient : IDI
    {
        Task<bool> IsAllInvitesInHubAsync(int firmId);
    }
}