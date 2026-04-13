using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequestsBan;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationRequestsBan
{
    public interface IIntegrationRequestsBanApiClient
    {
        Task SetIsAllowedAsync(SetIsAllowedIntegrationRequestsBanRequestDto dto);
    }
}
