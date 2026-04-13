using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.CurrencyPayments;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.CurrencyPayments.Models;

namespace Moedelo.Money.ApiClient.PaymentOrders.CurrencyPayments
{
    [InjectAsSingleton(typeof(ICurrencyPaymentsApiClient))]
    public class CurrencyPaymentsApiClient : BaseApiClient, ICurrencyPaymentsApiClient
    {
        private const string dateFormat = "yyyy-MM-dd";

        public CurrencyPaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CurrencyPaymentsApiClient> logger)
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

        public async Task<IReadOnlyCollection<CurrencyPaymentOrderDto>> GetByPeriodAsync(PeriodRequestDto request)
        {
            var result = await GetAsync<ApiDataDto<IReadOnlyCollection<CurrencyPaymentOrderDto>>>(
                "/private/api/v1/PaymentOrders/Currency/ByPeriod",
                new
                {
                    StartDate = request.StartDate.ToString(dateFormat),
                    EndDate = request.EndDate.ToString(dateFormat)
                });
            
            return result.data;
        }
        
        public async Task<List<CurrencyBalanceDto>> BalanceOnDateAsync(DateTime date)
        {
            var result = await GetAsync<ApiDataDto<List<CurrencyBalanceDto>>>(
                "/private/api/v1/PaymentOrders/Currency/BalanceOnDate",
                new { date = date.ToString(dateFormat) });
            
            return result.data;
        }
        
        public async Task<IReadOnlyCollection<CurrencyPaymentOrderDto>> GetByBaseIdAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Array.Empty<CurrencyPaymentOrderDto>();
            }
            
            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataDto<IReadOnlyCollection<CurrencyPaymentOrderDto>>>(
                "/private/api/v1/PaymentOrders/Currency/ByBaseIds", 
                documentBaseIds);
            
            return result.data;
        }
    }
}