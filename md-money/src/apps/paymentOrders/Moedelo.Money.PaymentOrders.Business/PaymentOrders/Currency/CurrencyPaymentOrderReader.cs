using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Currency;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Currency
{
    [InjectAsSingleton(typeof(ICurrencyPaymentOrderReader))]
    internal class CurrencyPaymentOrderReader : ICurrencyPaymentOrderReader
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly ICurrencyPaymentOrderDao dao;
        private readonly IBalancesApiClient balancesApiClient;

        public CurrencyPaymentOrderReader(
            IExecutionInfoContextAccessor executionInfoContext,
            ICurrencyPaymentOrderDao dao,
            IBalancesApiClient balancesApiClient)
        {
            this.executionInfoContext = executionInfoContext;
            this.dao = dao;
            this.balancesApiClient = balancesApiClient;
        }
        
        public Task<IReadOnlyList<PaymentOrder>> ByPeriodAsync(PeriodRequest request)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return dao.ByPeriodAsync((int)context.FirmId, request);
        }

        public async Task<IReadOnlyList<CurrencyBalance>> BalanceOnDateAsync(DateTime date)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var balanceDate = await balancesApiClient.GetDateAsync(context.FirmId, context.UserId);
            return await dao.BalanceOnDateAsync((int)context.FirmId, date, balanceDate);
        }

        public Task<IReadOnlyList<PaymentOrder>> ByBaseIdsAsync(IReadOnlyCollection<long> baseIds)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return dao.ByBaseIdsAsync((int)context.FirmId, baseIds);
        }
    }
}