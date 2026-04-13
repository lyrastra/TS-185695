using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationRequests;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationRequests
{
    public interface IIntegrationRequestApiClient
    {
        Task<IntegrationRequestDto> GetLastIntegrationRequestByPartnerAsync(IntegrationPartners partner);

        Task<IntegrationRequestDto> GetByIdAsync(int requestId, CancellationToken cancellationToken);

        /// <returns>Идентификатор созданной записи в БД</returns>
        Task<int> CreateNewAsync(NewIntegrationRequestDto requestDto);

        /// <summary>
        /// Сменить статус и выставить дату запроса (если указана)
        /// </summary>
        /// <param name="requestId">идентификатор запроса</param>
        /// <param name="status">новый статус</param>
        /// <param name="dateOfCall">новое значение DateOfCall (null - если не нужно менять)</param>
        Task SetStatusAsync(int requestId, RequestStatus status, string dateOfCall);

        /// <summary>
        /// Обновить статус при условии, что текущий статус совпадает с ожидаемым
        /// </summary>
        /// <param name="requestId">идентификатор запроса</param>
        /// <param name="status">новый статус</param>
        /// <param name="oldStatus">текущий статус</param>
        Task UpdateStatusAsync(int requestId, RequestStatus status, RequestStatus oldStatus);

        /// <summary>
        /// Сменить статус и добавить записи в историю работы по запросу
        /// </summary>
        Task AddHistoryAsync(IntegrationRequestNewHistoryDto dto);

        Task UpdateIntegrationRequestsAsync(IntegrationRequestStatusesUpdateRequestDto request);

        Task<IReadOnlyList<FirmSettlementNumberDto>> GetSettlementNumbersWithoutRequestsInPeriodAsync(
            SettlementNumbersWithoutRequestsInPeriodRequestDto requestDto,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<FirmSettlementNumberOnDateDto>> GetFirmSettlementNumbersWithoutRequestsInPeriodAsync(
            MissedMovementsRequestsFilterDto requestDto,
            CancellationToken cancellationToken);

        Task<int> CountRequestsByFilterAsync(
            CountIntegrationRequestsByFilterDto requestDto,
            CancellationToken cancellationToken);
    }
}
