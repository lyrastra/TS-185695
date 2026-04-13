using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.YandexKassaPayments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.YandexKassaPayments
{
    [InjectAsSingleton]
    public class YandexKassaPaymentApiClient : BaseApiClient, IYandexKassaPaymentApiClient
    {
        private readonly SettingValue apiEndPoint;

        public YandexKassaPaymentApiClient(
           IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
           : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        public Task SaveYandexKassaPaymentAsync(YandexKassaPaymentDto dto)
        {
            return PostAsync("/YandexKassaPayment/Save", dto);
        }

        public Task<List<YandexKassaPaymentDto>> GetYandexKassaPaymentsAsync(GetYandexKassaPaymentsCriteriaRequest request)
        {
            return PostAsync<GetYandexKassaPaymentsCriteriaRequest, List<YandexKassaPaymentDto>>("/YandexKassaPayment/Get", request);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}
