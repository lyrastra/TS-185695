using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Currency
{
    public interface ICurrencyPaymentOrderReader
    {
        Task<IReadOnlyList<PaymentOrder>> ByPeriodAsync(PeriodRequest request);
        
        Task<IReadOnlyList<CurrencyBalance>> BalanceOnDateAsync(DateTime date);
        
        Task<IReadOnlyList<PaymentOrder>> ByBaseIdsAsync(IReadOnlyCollection<long> baseIds);
    }
}