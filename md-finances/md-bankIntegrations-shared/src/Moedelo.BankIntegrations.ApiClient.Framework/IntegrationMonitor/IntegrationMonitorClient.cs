using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationMonitor;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegrationMonitor
{
    [InjectAsSingleton]
    public class IntegrationMonitorClient : BaseApiClient, IIntegrationMonitorClient
    {
        private const string ControllerMetrics = "/metrics";
        private const string ControllerActions = "/actions";
        private const string ControllerCertificates = "/certificates";
        private const string ControllerBankingInformation = "/banking";
        private const string ControllerKafka = "/kafka";
        private const string ControllerHaProxy = "/haproxy";
        private readonly SettingValue apiEndPoint;

        public IntegrationMonitorClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("MonitorIntegrationApiEndPoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        /// <summary> статистика по выпискам за сутки без повторений (повторный перезапрос отменяет ошибки и предыдущие запросы)
        public Task<List<IntegrationRequestDto>> GetIntegrationRequestDayStatsWithoutRepeatAsync()
        {
            return GetAsync<List<IntegrationRequestDto>>($"{ControllerMetrics}/GetIntegrationRequestDayStatsWithoutRepeat");
        }

        /// <summary> прирост ошибок за сутки по сравнению с предыдущем днем 
        public Task<List<IntegrationRequestDto>> GetIntegrationRequestIncreaseErrorsAsync()
        {
            return GetAsync<List<IntegrationRequestDto>>($"{ControllerMetrics}/GetIntegrationRequestIncreaseErrors");
        }

        /// <summary> статистика по заданному типу за сутки с учетом повторовных запросов (повторный перезапрос не отменяет ничего)
        public Task<List<IntegrationRequestDto>> GetIntegrationRequestStatsByPeriodWithRepeatAsync(int integrationCallType)
        {
            return GetAsync<List<IntegrationRequestDto>>($"{ControllerMetrics}/GetIntegrationRequestStatsByPeriodWithRepeat?integrationCallType={integrationCallType}");
        }

        /// <summary> статистика последнего перезапроса пропущенных выписок
        public Task<List<IntegrationReRequestDto>> GetIntegrationReRequestStatsAsync()
        {
            return GetAsync<List<IntegrationReRequestDto>>($"{ControllerMetrics}/GetIntegrationReRequestStats");
        }

        /// <summary> статистика дневных запросов выписок (RequestAutoTodayMovement)
        public Task<List<IntegrationAutoTodayRequestDto>> GetIntegrationAutoTodayRequestStatsAsync()
        {
            return GetAsync<List<IntegrationAutoTodayRequestDto>>($"{ControllerMetrics}/GetIntegrationAutoTodayRequestStats");
        }

        /// <summary> Запрос аналитики по запросам из IntegrationRequest
        /// 3-и превышения процента допустимых ошибок считается проблемой->Много ошибок
        public Task<List<MetricErrorDto>> GetIntegrationRequestErrorStatsAsync()
        {
            return GetAsync<List<MetricErrorDto>>($"{ControllerMetrics}/GetIntegrationRequestErrorStats");
        }

        /// <summary> Запрос аналитики по метрики фэйковых запросов
        /// 3-и несупешных фэйк-запроса считаются ошибкой -> Не работает
        public Task<List<MetricErrorDto>> GetFakeRequestMovementErrorStatsAsync()
        {
            return GetAsync<List<MetricErrorDto>>($"{ControllerMetrics}/GetFakeRequestMovementErrorStats");
        }

        /// <summary> Можно отправить фэйк-запрос выписки из монитора
        public Task<StateFakeRequestMovementDto> SendFakeRequestMovementAsync(int partner)
        {
            return GetAsync<StateFakeRequestMovementDto>($"{ControllerActions}/SendFakeRequestMovement?partner={partner}");
        }

        public Task<List<CertificatePartnerDto>> GetPartnerCertificatesAsync()
        {
            return GetAsync<List<CertificatePartnerDto>>($"{ControllerCertificates}/GetPartnerCertificates");
        }

        public Task<List<BankingInformationDto>> GetBankingInformationsAsync()
        {
            return GetAsync<List<BankingInformationDto>>($"{ControllerBankingInformation}/GetBankingInformations");
        }

        public Task<List<HashRequestsMovementsDto>> GetStatisticsOnIncompleteStatementsAsync()
        {
            return GetAsync<List<HashRequestsMovementsDto>>($"{ControllerMetrics}/GetStatisticsOnIncompleteStatements");
        }

        public Task<List<IntegratedUserStatsDto>> GetIntegratedUserStatsAsync()
        {
            return GetAsync<List<IntegratedUserStatsDto>>($"{ControllerMetrics}/GetIntegratedUserStats");
        }

        public Task<List<IntegratedUserStatsDto>> GetIntegratedUserTrialStatsAsync()
        {
            return GetAsync<List<IntegratedUserStatsDto>>($"{ControllerMetrics}/GetIntegratedUserTrialStats");
        }

        public Task<List<IntegratedUserStatsDto>> GetIntegratedUserDisableStatsAsync()
        {
            return GetAsync<List<IntegratedUserStatsDto>>($"{ControllerMetrics}/GetIntegratedUserDisableStats");
        }

        public Task<List<IntegratedUserStatsDto>> GetIntegratedUserIncreaseDisableStatsAsync()
        {
            return GetAsync<List<IntegratedUserStatsDto>>($"{ControllerMetrics}/GetIntegratedUserIncreaseDisableStats");
        }

        /// <summary>Получить состояние Kafka consumer groups</summary>
        /// <param name="filter">Подстрока для фильтрации GroupId. Null — все группы.</param>
        public Task<List<KafkaConsumerStateDto>> GetKafkaConsumersStateAsync(string filter = null)
        {
            var url = $"{ControllerKafka}/GetConsumersState";
            if (!string.IsNullOrWhiteSpace(filter))
                url += $"?filter={Uri.EscapeDataString(filter)}";
            return GetAsync<List<KafkaConsumerStateDto>>(url);
        }

        /// <summary>Получить статусы всех backend-серверов HAProxy</summary>
        public Task<List<HaProxyBackendStatusDto>> GetHaProxyStatusesAsync()
        {
            return GetAsync<List<HaProxyBackendStatusDto>>($"{ControllerHaProxy}/GetHaProxyStatuses");
        }
    }
}