using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentRemover))]
    internal class RentPaymentRemover : IRentPaymentRemover
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderRemover paymentOrderRemover;
        private readonly IRentPeriodDao rentPeriodDao;

        public RentPaymentRemover(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderRemover paymentOrderRemover,
            IRentPeriodDao rentPeriodDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderRemover = paymentOrderRemover;
            this.rentPeriodDao = rentPeriodDao;
        }

        public Task DeleteAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return Task.WhenAll(
                paymentOrderRemover.DeleteAsync(documentBaseId),
                rentPeriodDao.DeleteAsync((int)context.FirmId, documentBaseId));
        }
    }
}
