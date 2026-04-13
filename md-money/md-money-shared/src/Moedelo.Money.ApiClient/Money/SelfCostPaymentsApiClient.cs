using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.Money;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Money
{
    [InjectAsSingleton(typeof(ISelfCostPaymentsApiClient))]
    internal sealed class SelfCostPaymentsApiClient : BaseApiClient, ISelfCostPaymentsApiClient
    {
        public SelfCostPaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SelfCostPaymentsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiEndpoint"),
                logger)
        {
        }

        public async Task<IReadOnlyCollection<SelfCostPaymentDto>> GetSettlementAccountPaymentsAsync(SelfCostPaymentRequestDto requestDto)
        {
            var response = await PostAsync<SelfCostPaymentRequestDto, ApiDataDto<List<SelfCostPaymentDto>>>(
                "/private/api/v1/SelfCostPayments/SettlementAccountPayments", 
                requestDto);
            return response.data;
        }

        public async Task<IReadOnlyCollection<SelfCostPaymentDto>> GetCashPaymentsAsync(SelfCostPaymentRequestDto requestDto)
        {
            var response = await PostAsync<SelfCostPaymentRequestDto, ApiDataDto<List<SelfCostPaymentDto>>>(
                "/private/api/v1/SelfCostPayments/CashPayments",
                requestDto);
            return response.data;
        }

        public async Task<IReadOnlyCollection<SelfCostPaymentDto>> GetCurrencyNdsPaymentsAsync(SelfCostPaymentRequestDto requestDto)
        {
            var response = await PostAsync<SelfCostPaymentRequestDto, ApiDataDto<List<SelfCostPaymentDto>>>(
                "/private/api/v1/SelfCostPayments/CurrencyNdsPayments",
                requestDto);
            return response.data;
        }
    }
}