using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SberbankCryptoEndpointV2.Dto.SberbankMoedeloToken;
using System.Threading.Tasks;

namespace Moedelo.SberbankCryptoEndpointV2.Client.RedisSberbankTokenClient
{
    /// <summary> Клиент для работы с токенам инашего главного пользователя СББОЛ </summary>
    public interface IRedisSberbankTokenClient : IDI
    {
        Task<SberbankMoedeloTokenDto> GetTokenFromRedisByClientIdAsync(string clientId);
        Task<SberbankMoedeloTokenSaveResponseDto> SaveTokenToRedisAsync(SberbankMoedeloTokenDto dto);
    }
}
