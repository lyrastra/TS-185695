using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Private;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Currency
{
    public interface ICurrencyPaymentOrderGetter
    {
        Task<IReadOnlyCollection<CurrencyPaymentOrder>> ByPeriodAsync(PeriodRequest request);

        Task<IReadOnlyCollection<CurrencyPaymentOrder>> ByBaseIdsAsync(IReadOnlyCollection<long> baseIds);
        
        Task<IReadOnlyCollection<CurrencyBalance>> BalanceOnDateAsync(DateTime date);
    }
}