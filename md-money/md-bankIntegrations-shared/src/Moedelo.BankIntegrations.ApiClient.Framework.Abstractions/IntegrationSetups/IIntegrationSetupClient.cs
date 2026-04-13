using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationSetup;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationSetups;

public interface IIntegrationSetupClient
{
    /// <summary>
    /// Запустить цепочку по отключению интеграции
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<IntegrationDisableResponseDto> RunDisableAsync(IntegrationDisableRequestDto dto);
}