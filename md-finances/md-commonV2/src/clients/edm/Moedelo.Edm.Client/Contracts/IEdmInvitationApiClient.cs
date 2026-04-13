using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;
using Moedelo.Edm.Dto;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IEdmInvitationApiClient : IDI
    {
        Task ResetInviteStatus(int id, int firmId);
        Task<bool> SendInviteToMoedeloOrGlavuchetAsync(int firmId, int userId);
        Task<BaseDto> InviteByGlobalIdAsync(int kontragentId, string globalId, int firmId, int userId);
        Task<bool> RefuseInvitationAsync(int kontragentId, int firmId);
        Task<bool> SendInviteToMoedeloAsync(int firmId, int userId);
        Task<bool> SendInviteToGlavuchetAsync(int firmId, int userId, int paymentId);
    }
}
