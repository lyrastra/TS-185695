using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Private;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.ApiClient
{
    public interface ICurrencyPaymentOrderApiClient
    {
        Task<IReadOnlyCollection<CurrencyPaymentOrder>> ByPeriodAsync(PeriodRequest request);

        Task<IReadOnlyCollection<CurrencyBalance>> BalanceOnDateAsync(DateTime date);
        
        Task<IReadOnlyCollection<CurrencyPaymentOrder>> ByBaseIdsAsync(IReadOnlyCollection<long> baseIds);
    }
}