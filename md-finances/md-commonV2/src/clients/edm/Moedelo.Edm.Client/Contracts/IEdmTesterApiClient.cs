using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Kontragents;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IEdmTesterApiClient : IDI
    {
        Task SetUpInvitationAsync(int firmId, int kontragentId, string provider, KontragentEdmInteractionStatus status);
        Task SendInviteAsync(int firmId, int userId, int kontragentId, string provider);
    }
}
