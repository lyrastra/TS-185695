using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.IntegrationRequest;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.IntegrationRequest
{
    /// <summary> API для работы с ошибками интеграций </summary>
    public interface IIntegrationRequestClient : IDI
    {
        /// <summary>
        /// Установить статус Ошибка созданным запросам
        /// </summary>
        /// <param name="dto"> Параметр с диапазоном дат и айди фирмы </param>
        /// <returns></returns>
        Task ResetCreatedRequestsToErrorStatus(IntegrationRequestResetDto dto);
    }
}