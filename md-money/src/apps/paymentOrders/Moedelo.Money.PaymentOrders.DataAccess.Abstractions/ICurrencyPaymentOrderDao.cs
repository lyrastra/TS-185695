using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface ICurrencyPaymentOrderDao
    {
        Task<IReadOnlyList<PaymentOrder>> ByPeriodAsync(int firmId, PeriodRequest request);

        Task<IReadOnlyList<CurrencyBalance>> BalanceOnDateAsync(int firmId, DateTime date, DateTime? balanceDate);
        
        Task<IReadOnlyList<PaymentOrder>> ByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds);
    }
}