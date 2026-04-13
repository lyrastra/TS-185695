using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Finances.Client.Money
{
    [InjectAsSingleton(typeof(IUnrecognizedPaymentsClient))]
    public class UnrecognizedPaymentsClient : BaseApiClient, IUnrecognizedPaymentsClient
    {
        private readonly SettingValue apiEndpoint;

        public UnrecognizedPaymentsClient(
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

        public Task<int> GetCountAsync(int firmId, int userId, MoneySourceType? sourceType = null, long? sourceId = null)
        {
            return GetAsync<int>(
                $"/Money/UnrecognizedPayments/Count",
                new { firmId, userId, sourceType, sourceId }
            );
        }
    }
}