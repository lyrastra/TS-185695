using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsRemover))]
    class PaymentToNaturalPersonsRemover : IPaymentToNaturalPersonsRemover
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderRemover paymentOrderRemover;
        private readonly IWorkerPaymentDao workerPaymentDao;

        public PaymentToNaturalPersonsRemover(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderRemover paymentOrderRemover,
            IWorkerPaymentDao workerPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderRemover = paymentOrderRemover;
            this.workerPaymentDao = workerPaymentDao;
        }

        public Task DeleteAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return Task.WhenAll(
                paymentOrderRemover.DeleteAsync(documentBaseId),
                workerPaymentDao.DeleteAsync((int)context.FirmId, documentBaseId));
        }
    }
}
