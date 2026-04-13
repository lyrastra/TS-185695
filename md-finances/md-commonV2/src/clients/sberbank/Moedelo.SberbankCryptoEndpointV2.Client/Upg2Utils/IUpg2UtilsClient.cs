using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.SberbankCryptoEndpointV2.Dto.Upg2Utils;

namespace Moedelo.SberbankCryptoEndpointV2.Client.Upg2Utils
{
    public interface IUpg2UtilsClient : IDI
    {
        Task<GetActiveSberbankCertificateResponseDto> GetActiveSberbankCertificateAsync();
        Task<bool> GetIsWorkingAsync();
        Task<string> TestGreenTokenAsync();
    }
}