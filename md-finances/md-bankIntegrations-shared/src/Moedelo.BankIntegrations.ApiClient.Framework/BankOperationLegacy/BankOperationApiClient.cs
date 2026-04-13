using System.Collections.Generic;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperationLegacy;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;

namespace Moedelo.BankIntegrations.ApiClient.Framework.BankOperationLegacy
{
    [InjectAsSingleton(typeof(IBankOperationApiClient))]
    public class BankOperationApiClient: BaseCoreApiClient, IBankOperationApiClient
    {
        private readonly ISettingRepository settingRepository;

        public BankOperationApiClient(
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
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("IntegrationApi").Value;
        }
        public async Task<SendPaymentOrderResponseDto> SendPaymentOrdersAsync(List<PaymentOrderDto> paymentOrders, IntegrationIdentityDto identity)
        {
            var dto = new SendPaymentOrderRequestDto { PaymentOrders = paymentOrders, Identity = identity };
            return (await PostAsync<SendPaymentOrderRequestDto, IntegrationResponseDto<SendPaymentOrderResponseDto>>("/BankOperation/SendPaymentOrders",
                dto, setting: new HttpQuerySetting(timeout: TimeSpan.FromMinutes(2))).ConfigureAwait(false)).Data;
        }
    }
}