using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderTaxWidgetReader))]
    internal class PaymentOrderTaxWidgetReader : IPaymentOrderTaxWidgetReader
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IPaymentOrderTaxWidgetDao dao;

        public PaymentOrderTaxWidgetReader(
            IExecutionInfoContextAccessor executionInfoContext,
            IPaymentOrderTaxWidgetDao dao)
        {
            this.executionInfoContext = executionInfoContext;
            this.dao = dao;
        }

        public async Task<IReadOnlyCollection<OrderTaxWidgetResponse>> GetBudgetaryTaxPaymentsAsync(BudgetaryPaymentTaxWidgetRequest request)
        {
            var orders = await dao.GetBudgetaryTaxPaymentsAsync((int)executionInfoContext.ExecutionInfoContext.FirmId, request);

            return orders.Select(item => new OrderTaxWidgetResponse
            {
                DocumentBaseId = item.DocumentBaseId,
                Date = item.Date,
                Sum = item.Sum,
                PeriodType = item.PeriodType,
                PeriodNumber = item.PeriodNumber
            }).ToArray();
        }
    }
}
