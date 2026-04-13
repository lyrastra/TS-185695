using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SberbankCryptoEndpointV2.Dto.Sso;

namespace Moedelo.SberbankCryptoEndpointV2.Client.Sso
{
    public interface ISsoClient : IDI
    {
        Task<bool> UpdateIntegrationDataAsync(IntegrationDataDto dto);

        Task<AccessTokenResponseDto> GetAccessTokenV2Async(AccessTokenRequestDto dto);

        Task<UserInfoResponseDto> GetUserInfoV2Async(UserInfoRequestDto dto);
    }
}