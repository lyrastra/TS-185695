using System.Threading.Tasks;

using Moedelo.HomeV2.Dto.Registration;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.Registration
{
    public interface IRegistrationApiClient : IDI
    {
        Task<RegistrationResponseDto> RegisterForEvotorAsync(int firmId, int userId, RegistrationRequestDto dto);
        
        Task<BaseRegistrationResponseDto> RegisterForAccountAsync(int firmId, int userId, RegistrationDto dto);

        Task<UserFirmResponseDto> SaveUserFirmAsync(int firmId, int userId, UserFirmDto dto, string host);

        Task<UserFirmResponseDto> AttachUserForAccountAsync(int firmId, int userId, UserFirmDto dto, string host);
    }
}