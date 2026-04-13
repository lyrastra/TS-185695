using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;


namespace Moedelo.Finances.Client.CloseCashGroup
{
    [InjectAsSingleton]
    public class CloseCashGroupClient : BaseApiClient,  ICloseCashGroupClient
    {
        private readonly SettingValue apiEndpoint;

        public CloseCashGroupClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FinancesPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        /// <summary>
        /// Возвращает последнюю дату закрытой кассы для пользователей БИЗ
        /// </summary>
        /// <returns>Возвращает дату или, если касса еще не закрывалась - null</returns>
        public Task<DateTime?> GetLastClosedCashGroupDateAsync(int firmId, int userId)
        {
            return GetAsync<DateTime?>("/ClosedCashGroup/LastClosedCashDate", new { firmId, userId });
        }
    }
}
