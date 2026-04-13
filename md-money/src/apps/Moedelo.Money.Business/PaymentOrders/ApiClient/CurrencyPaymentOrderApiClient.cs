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
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.ApiClient;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.Domain.PaymentOrders.Private;

namespace Moedelo.Money.Business.PaymentOrders.ApiClient
{
    [InjectAsSingleton(typeof(ICurrencyPaymentOrderApiClient))]
    public class CurrencyPaymentOrderApiClient : BaseApiClient, ICurrencyPaymentOrderApiClient
    {
        private const string dateFormat = "yyyy-MM-dd";
        private const string prefix = "/private/api/v1";
        private static readonly HttpQuerySetting PaymentOrderDefaultSetting = new HttpQuerySetting(TimeSpan.FromSeconds(30));

        public CurrencyPaymentOrderApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<CurrencyPaymentOrderApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PaymentOrderApiEndpoint"),
                logger)
        {
        }
        
        public async Task<IReadOnlyCollection<CurrencyPaymentOrder>> ByPeriodAsync(PeriodRequest request)
        {
            var response = await GetAsync<ApiDataResponseWrapper<IReadOnlyCollection<CurrencyPaymentOrder>>>(
                $"{prefix}/PaymentOrders/Currency/ByPeriod", new
                {
                    StartDate = request.StartDate.ToString(dateFormat),
                    EndDate = request.EndDate.ToString(dateFormat)
                }, setting: PaymentOrderDefaultSetting);
            return response.Data;
        }

        public async Task<IReadOnlyCollection<CurrencyBalance>> BalanceOnDateAsync(DateTime date)
        {
            var response = await GetAsync<ApiDataResponseWrapper<IReadOnlyCollection<CurrencyBalance>>>(
                $"{prefix}/PaymentOrders/Currency/BalanceOnDate",
                new { date = date.ToString(dateFormat) },
                setting: PaymentOrderDefaultSetting);
            return response.Data;
        }

        public async Task<IReadOnlyCollection<CurrencyPaymentOrder>> ByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Array.Empty<CurrencyPaymentOrder>();
            }
            
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResponseWrapper<IReadOnlyCollection<CurrencyPaymentOrder>>>(
                $"{prefix}/PaymentOrders/Currency/ByBaseIds", baseIds, setting: PaymentOrderDefaultSetting);
            return response.Data;
        }
    }
}