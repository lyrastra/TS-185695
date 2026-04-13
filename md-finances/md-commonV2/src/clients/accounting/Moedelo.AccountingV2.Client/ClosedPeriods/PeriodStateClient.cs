using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.ClosedPeriods
{
    /// <summary>
    /// Функционал актуален только для ИП.
    /// Изначально это API для вызова в мастерах авансовы платежи по УСН и Декларации УСН. 
    /// Алгоритм должен (пере)расчитывать данные учета с начала года до конца указанного квартала
    /// </summary>
    [InjectAsSingleton]
    public class PeriodStateClient : BaseApiClient, IPeriodStateClient
    {
        private readonly SettingValue endpoint;

        public PeriodStateClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            this.endpoint = settingRepository.Get("AccountingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public Task SaveQuarterAsync(int firmId, int userId, int year, int quarter)
        {
            return PostAsync<Task>(
                $"/PeriodState/SaveQuarter?firmId={firmId}&userId={userId}&year={year}&quarter={quarter}",
                null,
                new HttpQuerySetting
                {
                    Timeout = TimeSpan.FromMinutes(2)
                }
            );
        }
    }
}
