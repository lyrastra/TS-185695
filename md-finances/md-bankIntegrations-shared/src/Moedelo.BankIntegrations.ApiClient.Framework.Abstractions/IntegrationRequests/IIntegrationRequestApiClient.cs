using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationRequests
{
    public interface IIntegrationRequestApiClient
    {
        /// <summary>
        /// Получить интеграционнй запрос по паре (partner, requestId) для указаной фирмы
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="partner"><see cref="IntegrationRequestDto.RequestId"/></param>
        /// <param name="requestId"><see cref="IntegrationRequestDto.IntegrationPartner"/></param>
        /// <returns>интеграционный запрос</returns>
        Task<IntegrationRequestDto> GetAsync(int firmId, IntegrationPartners partner, string requestId);

        /// <summary>
        /// Получить список интеграционных запросов, удовлетворяющих заданным условиям
        /// </summary>
        /// <param name="filterDto">условия</param>
        /// <returns>список интеграционных запросов</returns>
        Task<PaginatedCollectionDto<IntegrationRequestDto>> GetListAsync(IntegrationRequestsListFilterDto filterDto);

        /// <summary>
        /// Получить интеграционнй запрос по идентификатору
        /// </summary>
        /// <param name="id">идентификатор запроса в БД</param>
        /// <returns>интеграционный запрос</returns>
        Task<IntegrationRequestDto> GetAsync(int id);

        Task<IntegrationRequestWithXmlHistoryDto[]> GetWithXmlHistoryAsync(
            IntegrationRequestsWithXmlHistoryFilterDto filterDto);

        /// <summary>
        /// Проверить, есть ли интеграционнй запрос, удовлетворяющий условиям
        /// </summary>
        /// <param name="conditionsDto">идентификатор запроса в БД</param>
        /// <returns>true/false</returns>
        Task<bool> HasAsync(HasIntegrationRequestsCheckConditionsDto conditionsDto);

        /// <summary>
        /// Сменить статус и выставить дату запроса
        /// </summary>
        /// <param name="requestId">идентификатор запроса</param>
        /// <param name="status">новое значение статуса</param>
        /// <param name="dateOfCall">новое значение DateOfCall</param>
        Task SetStatusAsync(int requestId, RequestStatus status, string dateOfCall);

        /// <summary>
        /// Сменить статус и добавить записи в историю работы по запросу
        /// </summary>
        /// <param name="dto">параметры вызова</param>
        Task AddHistoryAsync(IntegrationRequestNewHistoryDto dto);

        /// <summary>
        /// Массовое выставление статуса запросам и подзапросам
        /// </summary>
        /// <returns></returns>
        Task SetStatusAsync(SetIntegrationRequestsStatusDto requestDto);

        /// <summary>
        /// Создать новый интеграционный запрос
        /// </summary>
        /// <param name="newRequestDto">параметры создания</param>
        /// <returns>идентификатор запроса</returns>
        Task<int> CreateNewAsync(NewIntegrationRequestDto newRequestDto);

        /// <summary>
        /// Посчитать количество запросов заданных статусов для указанных партнёров
        /// </summary>
        /// <param name="dto">параметры запроса</param>
        /// <returns>список счётчиков по парам (партнёр, статус запроса)</returns>
        Task<List<IntegrationPartnerRequestsInStatusCountDto>> CountPartnerRequestsAsync(PartnerIntegrationRequestsCountClaimDto dto);
        
        /// <summary>
        /// Получить информацию о партнёрах, у которых есть необработанные запросы выписок
        /// </summary>
        /// <param name="dto">параметры запроса</param>
        /// <returns>список партнёров</returns>
        Task<List<IntegrationPartners>> HasUnprocessedRequestMovementListAsync(HasUnprocessedRequestMovementListDto dto);

        /// <summary>
        /// todo: заполнить описание
        /// </summary>
        /// <param name="dto">параметры запроса</param>
        /// <returns>список счётчиков по парам (партнёр, статус запроса)</returns>
        Task<List<IntegrationPartnerRequestsInStatusCountDto>> CountPartnerRequestsAsync(PartnerIntegrationRequestDailyWithoutRepeatClaimDto dto);

        /// <summary>
        /// todo: заполнить описание
        /// </summary>
        /// <param name="dto">параметры запроса</param>
        /// <returns>список счётчиков</returns>
        Task<List<IntegrationPartnerRequestsInStatusCountDto>> CountPartnerRequestsAsync(PartnerIntegrationRequestsCountFilterDto dto);

        /// <summary>
        /// Получить дату последнего интеграционного запроса, удовлетворяющего условиям
        /// </summary>
        /// <param name="dto">условия</param>
        /// <returns>дата (значение поля EndDate)</returns>
        Task<DateTime?> GetLastIntegrationRequestEndDateAsync(LastPartnerIntegrationRequestDateClaimDto dto);
        
        /// <summary>
        /// Запрос пропущенных субботних выписок
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        Task SendMissedMovementsRequestAsync(
            RequestMovementsMissingDaysRequestDto requestDto);
    }
}
