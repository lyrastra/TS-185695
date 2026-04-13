using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.DataInformation.Sberbank;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.DataInformation
{
    public interface IBankIntegrationsDataInformationClient : IDI
    {
        /// <summary> Имеются ли необработанные запросы во всех банках </summary>
        Task<List<IntegrationRequestQueueStatusDto>> GetRequestQueueStatusAsync(int firmId);

        /// <summary> Имеются ли необработанные запросы в банк </summary>
        Task<bool> HasUnprocessedRequestsAsync(int firmId, IntegrationPartners integrationPartner);

        /// <summary> Получить дату создания р/сч в сбербанке </summary>
        Task<DateTime?> GetSberbankSettlementsFirstCreateDateAsync(int firmId);

        /// <summary> Получить дату последней успешной выписки </summary>
        Task<DateTime?> GetMovementLastDateAsync(IntegrationPartners integrationPartner, int firmId);

        /// <summary> Получить информацию по интеграциям по р/сч </summary>
        Task<IntSummaryBySettlementsResponseDto> GetIntSummaryBySettlementsAsync(IntSummaryBySettlementsRequestDto requestDto);

        /// <summary> Получить информацию по запросам для партнёрки </summary>
        Task<BackOfficeResponseDto> GetIntegrationRequestsToBackOfficeAsync(BackOfficeRequestDto dto);

        /// <summary> Получение файла логов </summary>
        Task<BackOfficeLogResponseDto> GetIntegrationLogToBackOfficeAsync(int requestId);

        /// <summary> Получение списка подключенных р/сч в Сбербанке </summary>
        Task<List<SettlementAccountV2Dto>> GetSberbankSettlementsAsync(int firmId);
        
        /// <summary> Получение списка подключенных р/сч у партнёра-банка </summary>
        Task<List<SettlementAccountV2Dto>> GetSettlementAccountsByPartnerAsync(int firmId, int userId, IntegrationPartners partner);

        /// <summary> Получить статус платежа, тправленного ранее в Сбербанк </summary>
        Task<GetSberbankPaymentStatusResponseDto> GetSberbankPaymentsStatusAsync(GetSberbankPaymentStatusRequestDto requestDto);
        
        /// <summary> Имеет ли клиент больше одного р/с у партнёра </summary>
        Task<bool> IsMoreThanOneSettlementAccountAsync(int firmId, int userId, IntegrationPartners partner);
    }
}