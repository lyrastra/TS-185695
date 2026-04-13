using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationSetup;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;

public interface IIntegrationSetupClient
{
    /// <summary>
    /// Запустить типовую цепочку методов по включению интеграции
    /// </summary>
    public Task<IntegrationSetupResponseDto> RunEnableAsync(IntegrationEnableSetupRequestDto dto);

    /// <summary>
    /// Запустить типовую цепочку методов по выключению интеграции
    /// </summary>
    Task<IntegrationDisableResponseDto> RunDisableAsync(IntegrationDisableRequestDto dto);
}