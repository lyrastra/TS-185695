using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto.PaymentMethods;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework
{
    [InjectAsSingleton(typeof(IPaymentMethodsApiClient))]
    public class PaymentMethodsApiClient : BaseApiClient, IPaymentMethodsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PaymentMethodsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BillingPaymentMethodsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<IReadOnlyCollection<PaymentMethodDto>> GetByCriteriaAsync(PaymentMethodSearchCriteriaDto dto)
        {
            return PostAsync<PaymentMethodSearchCriteriaDto, IReadOnlyCollection<PaymentMethodDto>>("/Rest/PaymentMethods/GetByCriteria", dto);
        }

        public Task<IReadOnlyCollection<PaymentMethodDto>> GetAllAsync()
        {
            return GetAsync<IReadOnlyCollection<PaymentMethodDto>>("/Rest/PaymentMethods/GetAll");
        }
    }
}
