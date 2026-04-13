using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.HistoricalLogs.Backoffice;
using Moedelo.HistoricalLogsV2.Dto.BackofficeLog;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HistoricalLogsV2.Client.BackofficeLog
{
    /// <summary>
    /// Клиент для логирования действий в партнёрке
    /// </summary>
    public interface IBackofficeLogClient : IDI
    {
        /// <summary>
        /// Отправить записи в журнал
        /// Отправка происходит в фоновой задаче, завершение которой не ожидается
        /// </summary>
        /// <param name="records"></param>
        void Log(IReadOnlyCollection<BackofficeLogRequestDto> records);

        /// <summary>
        /// Отправить записи в журнал
        /// Метод возвращает таску, в рамках которой происходит отправка записей
        /// </summary>
        /// <param name="records"></param>
        Task LogAsync(IReadOnlyCollection<BackofficeLogRequestDto> records);
        
        /// <summary>
        /// Логировать успешное выполнение действия
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="actionType">Тип действия</param>
        /// <param name="objectId">Идентификатор объекта, над которым производится дейсвтие</param>
        /// <param name="actionData">Дополнительная информация о действии</param>
        void LogSuccess(int userId, BackofficeLogActionType actionType, int? objectId = null, BackofficeLogActionDataDto actionData = null);
        Task LogSuccessAsync(int userId, BackofficeLogActionType actionType, int objectId, BackofficeLogActionDataDto actionData);
        
        /// <summary>
        /// Логировать выполнение действия с ошибкой
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="actionType">Тип действия</param>
        /// <param name="objectId">Идентификатор объекта, над которым производится дейсвтие</param>
        /// <param name="actionData">Дополнительная информация о действии</param>
        void LogError(int userId, BackofficeLogActionType actionType, int? objectId = null, BackofficeLogActionDataDto actionData = null);
    }
}