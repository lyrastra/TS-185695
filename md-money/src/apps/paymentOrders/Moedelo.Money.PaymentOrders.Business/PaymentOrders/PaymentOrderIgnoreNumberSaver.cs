using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderIgnoreNumberSaver))]
    internal class PaymentOrderIgnoreNumberSaver : IPaymentOrderIgnoreNumberSaver
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IPaymentOrderDao dao;

        public PaymentOrderIgnoreNumberSaver(
            IExecutionInfoContextAccessor executionInfoContext,
            IPaymentOrderDao dao)
        {
            this.executionInfoContext = executionInfoContext;
            this.dao = dao;
        }

        public Task ApplyIgnoreNumberAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            return dao.ApplyIgnoreNumberAsync((int)context.FirmId, documentBaseIds);
        }
    }
}
