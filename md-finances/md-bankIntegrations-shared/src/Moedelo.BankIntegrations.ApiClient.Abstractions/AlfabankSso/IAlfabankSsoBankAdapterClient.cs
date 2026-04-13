using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.AlfabankSso;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.AlfabankSso;
using Moedelo.BankIntegrations.Dto.InitIntegration;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.AlfabankSso
{
    public interface IAlfabankSsoBankAdapterClient
    {
        Task<ApiDataResult<AlfaClientInfoDto>> GetClientInfoAsync(string code, string redirectUri, HttpQuerySetting setting = null);
        Task<ApiDataResult<InitIntegrationResponseDto>> InitIntegration(InitIntegrationRequestDto request, HttpQuerySetting setting = null);
        Task UpdateIntegrationData(UpdateIntegrationDataDto request, HttpQuerySetting setting = null);
    }
}
