using Moedelo.BankIntegrations.ApiClient.Dto.SovcomBankWl;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.SovcombankWl
{
    public interface ISovcomBankWlAdapterClient
    {
        Task<ClientInfoResponseDto> GetClientInfoAsync(string clientHashId, string state, HttpQuerySetting setting = null);
        Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto);
        Task<SubscribeStatusResponseDto> SetSubscribeStatusAsync(SubscribeStatusRequestDto subscribeStatusRequest, HttpQuerySetting setting = null);
    }

}