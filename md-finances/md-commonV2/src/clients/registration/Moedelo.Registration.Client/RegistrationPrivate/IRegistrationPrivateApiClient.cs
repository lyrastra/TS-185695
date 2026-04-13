using System.Threading.Tasks;
using Moedelo.Registration.Dto;

namespace Moedelo.Registration.Client.RegistrationPrivate
{
    public interface IRegistrationPrivateApiClient
    {
        Task<RegisterForAccountResponseDto> RegisterForAccountAsync(int firmId, int userId, RegisterForAccountRequestDto dto);
    }
}