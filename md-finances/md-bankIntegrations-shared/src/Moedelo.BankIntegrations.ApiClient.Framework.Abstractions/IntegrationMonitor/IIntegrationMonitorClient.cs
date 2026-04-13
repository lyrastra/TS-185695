using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationMonitor
{
    public interface IIntegrationMonitorClient : IDI
    {
        /// <summary> статистика по выпискам за 24ч, учитывается только последний запрос по фирме/счету
        Task<List<IntegrationRequestDto>> GetIntegrationRequestDayStatsWithoutRepeatAsync();

        /// <summary> прирост ошибок за сутки по сравнению с предыдущем днем 
        Task<List<IntegrationRequestDto>> GetIntegrationRequestIncreaseErrorsAsync();

        /// <summary> Статистика по выбраннуому типу с учетом всех перезапросов. т.е. 
        /// Несколько перезапросов будут сумироваться в результате
        Task<List<IntegrationRequestDto>> GetIntegrationRequestStatsByPeriodWithRepeatAsync(int integrationCallType);
        Task<List<IntegrationReRequestDto>> GetIntegrationReRequestStatsAsync();

        /// <summary> Статистика дневных запросов выписок (RequestAutoTodayMovement)
        Task<List<IntegrationAutoTodayRequestDto>> GetIntegrationAutoTodayRequestStatsAsync();

        /// <summary> Метрика статистики по выпискам за 24ч, трижды превышение заданного процента отображается в метрике как ошибка
        Task<List<MetricErrorDto>> GetIntegrationRequestErrorStatsAsync();

        /// <summary> Запрос аналитики по метрики фэйковых запросов
        /// 3-и несупешных фэйк-запроса считаются ошибкой -> Не работает
        Task<List<MetricErrorDto>> GetFakeRequestMovementErrorStatsAsync();

        /// <summary> Можно отправить фэйк-запрос выписки из монитора
        Task<StateFakeRequestMovementDto> SendFakeRequestMovementAsync(int partner);

        Task<List<CertificatePartnerDto>> GetPartnerCertificatesAsync();

        Task<List<BankingInformationDto>> GetBankingInformationsAsync();

        /// <summary> Статистика по неполным выпискам (сервиса HashRequests)</summary>
        Task<List<HashRequestsMovementsDto>> GetStatisticsOnIncompleteStatementsAsync();

        /// <summary>Статистика по подключенным интеграциям</summary>
        Task<List<IntegratedUserStatsDto>> GetIntegratedUserStatsAsync();

        /// <summary>Статистика по подключенным интеграциям триал и фримиум</summary>
        Task<List<IntegratedUserStatsDto>> GetIntegratedUserTrialStatsAsync();
        
        /// <summary>Статистика по отключенным интеграциям</summary>
        Task<List<IntegratedUserStatsDto>> GetIntegratedUserDisableStatsAsync();
        Task<List<IntegratedUserStatsDto>> GetIntegratedUserIncreaseDisableStatsAsync();

        /// <summary>Получить состояние Kafka consumer groups</summary>
        /// <param name="filter">Подстрока для фильтрации GroupId. Null — все группы.</param>
        Task<List<KafkaConsumerStateDto>> GetKafkaConsumersStateAsync(string filter = null);

        /// <summary>Получить статусы backend-серверов интеграции из HAProxy</summary>
        Task<List<HaProxyBackendStatusDto>> GetHaProxyStatusesAsync();
    }
}