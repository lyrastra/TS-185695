using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.IntegrationError;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.IntegrationError
{
    /// <summary> API для работы с ошибками интеграций </summary>
    public interface IIntegrationErrorClient : IDI
    {
        /// <summary>
        /// Получить список ошибок интеграций
        /// </summary>
        /// <param name="dto"> Параметр для получения ошибок интеграций </param>
        /// <returns> Список ошибок интеграций </returns>
        Task<IntegrationErrorListDto> GetListAsync(IntegrationErrorRequestDto dto);

        /// <summary>
        /// Установить статус Прочитан ошибки интеграции
        /// </summary>
        /// <param name="dto"> Параметр с идентификаторами ошибок и идентификатором фирмы</param>
        /// <returns></returns>
        Task SetReadStateAsync(IntegrationErrorSetReadDto dto);
    }
}