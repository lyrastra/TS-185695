using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.Authentication;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.Authentication
{
    public interface IAuthenticationApiClient : IDI
    {
        Task<UserVerificationResponseDto> VerifyAsync(int firmId, int userId, UserVerificationRequestDto dto);
    }
}