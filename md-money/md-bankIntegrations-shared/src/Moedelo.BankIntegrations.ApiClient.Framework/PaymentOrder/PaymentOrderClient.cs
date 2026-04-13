using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.PaymentOrder;
using Moedelo.BankIntegrations.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.PaymentOrder
{
    [InjectAsSingleton(typeof(IPaymentOrderClient))]
    public class PaymentOrderClient : BaseCoreApiClient, IPaymentOrderClient
    {
        private readonly SettingValue endpoint;
        
        public PaymentOrderClient(
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
            this.endpoint = settingRepository.Get("PaymentOrderApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task<SendPaymentOrderResponseDto> SendPaymentOrderAsync(SendPaymentOrderRequestDto data)
        {
            var response = await PostAsync<SendPaymentOrderRequestDto, ApiDataResult<SendPaymentOrderResponseDto>>(
                uri: "/private/api/v1/PaymentOrder/SendPaymentOrder", data,
                setting: new HttpQuerySetting(timeout: TimeSpan.FromMinutes(2)));

            return response.data;
        }
    }
}