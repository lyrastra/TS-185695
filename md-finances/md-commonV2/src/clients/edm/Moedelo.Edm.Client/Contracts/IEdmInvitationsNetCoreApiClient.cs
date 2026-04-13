using System.Threading.Tasks;
using Moedelo.Edm.Dto.Invitations;

namespace Moedelo.Edm.Client.Contracts
{
    public interface IEdmInvitationsNetCoreApiClient
    {
        Task<GetEnabledResponseDto> GetEnabledKontragentByIdsAsync(int firmId, int userId,
            GetEnabledRequestDto request);
    }
}
