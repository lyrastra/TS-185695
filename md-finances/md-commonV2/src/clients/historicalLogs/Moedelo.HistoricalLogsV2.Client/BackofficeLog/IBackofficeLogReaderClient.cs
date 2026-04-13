using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto.BackofficeLog;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HistoricalLogsV2.Client.BackofficeLog
{
    /// <summary>
    /// Клиент для извлечения логов действий в партнёрке
    /// </summary>
    public interface IBackofficeLogReaderClient : IDI
    {
        /// <summary>
        /// Получить список логов
        /// </summary>
        /// <param name="dto">Параметр для извлечения списка действий пользователей в партнерке</param>
        /// <returns>Список логов действия пользователей в партнерке</returns>
        Task<List<BackofficeLogDto>> GetListAsync(BackofficeLogParameterDto dto);

        /// <summary>
        /// Получить список логов для объектов
        /// </summary>
        /// <param name="dto">Параметр для извлечения списка действий пользователей в партнерке</param>
        /// <returns>Список логов действия пользователей в партнерке для объектов</returns>
        Task<List<BackofficeLogDto>> GetListForObjectsAsync(BackofficeLogParameterByObjectIdsDto dto);

        /// <summary>
        /// Получить дополнительную информацию о логе
        /// </summary>
        /// <param name="id">Идентификатор лога</param>
        /// <returns>Дополнительная информация о логе</returns>
        Task<BackofficeLogActionDataDto> GetActionDataAsync(int id);
    }
}