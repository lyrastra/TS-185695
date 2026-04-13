using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.Money
{
    public interface IOperationsValidationApiClient
    {
        /// <summary>
        /// Проверяет операции по базовым идентификаторам документов.
        /// </summary>
        /// <param name="query">
        /// Запрос с параметрами проверки:<br/>
        /// - IsAllPaid: если true — проверяется оплата;<br/>
        /// - IsPassedOutsourcingCheck: если true — проверяется статус аутсорсинговой обработки.
        /// </param>
        /// <param name="querySettings">Настройки HTTP-запроса timeout.</param>
        /// <returns>
        /// Список статусов валидации документов:<br/>
        /// - DocumentBaseId — идентификатор операции;<br/>
        /// - IsValid — true, если операция оплачена и валидна (тип проверки определяется параметрами запроса).
        /// </returns>

        Task<IReadOnlyList<DocumentValidationStatusDto>> CheckByBaseIdsAsync(DocumentsStatusQueryDto requestDto, HttpQuerySetting setting = null);
    }
}