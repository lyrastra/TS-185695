using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.PaymentRequest;
using Moedelo.BankIntegrations.Enums.Acceptance;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.PaymentRequest
{
    [InjectAsSingleton(typeof(ISberbankPaymentRequestApiClient))]
    internal sealed class SberbankPaymentRequestApiClient : BaseCoreApiClient, ISberbankPaymentRequestApiClient
    {
        private readonly SettingValue endpoint;
        private const string ControllerName = "/SberbankPaymentRequest/";
        
        public SberbankPaymentRequestApiClient(
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
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.endpoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value + ControllerName;
        }

        public async Task<bool> HasActiveAcceptanceByTypeAsync(int firmId, AcceptanceType type)
        {
            var query = new {firmId, Type = (int)type};
            return await GetAsync<bool>("HasActiveAcceptanceByType", query).ConfigureAwait(false);
        }

        /// <summary>
        /// Разблокировка ЗДА, они блокируются если отзывается согласие или инвалидируется токен, либо, если по р/с в ЗДА нет доступа и получили 403 от банка
        /// </summary>
        public async Task UnblockSberbankAcceptanceAsync(int firmId)
        {
            await PutAsync($"UnblockSberbankAcceptance?firmId={firmId}", new { }).ConfigureAwait(false);
        }
    }
}
