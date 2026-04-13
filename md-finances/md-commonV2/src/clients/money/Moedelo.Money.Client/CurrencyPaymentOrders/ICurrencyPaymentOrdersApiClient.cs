using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.CurrencyPaymentOrders;

namespace Moedelo.Money.Client.CurrencyPaymentOrders
{
    public interface ICurrencyPaymentOrdersApiClient : IDI
    {
        Task<List<CurrencyPaymentOrderDto>> ByPeriodAsync(int firmId, int userId, PeriodRequestDto request);

        Task<List<CurrencyBalanceDto>> BalanceOnDateAsync(int firmId, int userId, DateTime date);
    }
}