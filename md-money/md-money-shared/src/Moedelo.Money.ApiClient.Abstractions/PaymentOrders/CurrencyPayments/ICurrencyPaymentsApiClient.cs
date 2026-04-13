using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.CurrencyPayments.Models;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.CurrencyPayments
{
    public interface ICurrencyPaymentsApiClient
    {
        Task<IReadOnlyCollection<CurrencyPaymentOrderDto>> GetByPeriodAsync(PeriodRequestDto request);

        Task<List<CurrencyBalanceDto>> BalanceOnDateAsync(DateTime date);
        
        Task<IReadOnlyCollection<CurrencyPaymentOrderDto>> GetByBaseIdAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}