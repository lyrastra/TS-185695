using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationErrorLogs;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationErrorLog;

public interface IIntegrationErrorLogApiClient
{
    /// <summary>
    /// Создать новый интеграционный лог ошибки
    /// </summary>
    Task CreateAsync(IntegrationErrorLogDto dto);
}