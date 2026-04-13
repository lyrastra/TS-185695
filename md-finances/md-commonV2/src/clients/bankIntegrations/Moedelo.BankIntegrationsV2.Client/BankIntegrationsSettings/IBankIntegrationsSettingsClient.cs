using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.BankIntegrationsSettings
{
    /// <summary> API для работы с настройками банковских интеграций </summary>
    public interface IBankIntegrationsSettingsClient : IDI
    {
        /// <summary> Получение текущей настройки Success / Error ответа на запросы при mock-тестировании </summary>
        Task<bool> IsMockAllRequestErrorAsync();

        /// <summary> Переключение Success / Error ответа на запросы при mock-тестировании </summary>
        Task SetMockAllRequestErrorAsync(bool value);
    }
}