using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.ApiClient;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Currency;
using Moedelo.Money.Domain.PaymentOrders.Private;

namespace Moedelo.Money.Business.PaymentOrders.Currency
{
    [InjectAsSingleton(typeof(ICurrencyPaymentOrderGetter))]
    internal sealed class CurrencyPaymentOrderGetter : ICurrencyPaymentOrderGetter
    {
        private readonly ICurrencyPaymentOrderApiClient apiClient;

        public CurrencyPaymentOrderGetter(ICurrencyPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Task<IReadOnlyCollection<CurrencyPaymentOrder>> ByPeriodAsync(PeriodRequest request)
        {
            return apiClient.ByPeriodAsync(request);
        }

        public Task<IReadOnlyCollection<CurrencyPaymentOrder>> ByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            return apiClient.ByBaseIdsAsync(baseIds);
        }

        public Task<IReadOnlyCollection<CurrencyBalance>> BalanceOnDateAsync(DateTime date)
        {
            return apiClient.BalanceOnDateAsync(date);
        }
    }
}