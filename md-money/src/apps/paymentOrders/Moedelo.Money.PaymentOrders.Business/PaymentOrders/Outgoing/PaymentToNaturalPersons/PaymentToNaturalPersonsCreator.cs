using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsCreator))]
    class PaymentToNaturalPersonsCreator : IPaymentToNaturalPersonsCreator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderCreator paymentOrderCreator;
        private readonly IWorkerPaymentDao workerPaymentDao;

        public PaymentToNaturalPersonsCreator(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderCreator paymentOrderCreator,
            IWorkerPaymentDao workerPaymentDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderCreator = paymentOrderCreator;
            this.workerPaymentDao = workerPaymentDao;
        }

        public async Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var response = await paymentOrderCreator.CreateAsync(request).ConfigureAwait(false);
            await workerPaymentDao.InsertAsync((int)context.FirmId, request.DocumentBaseId, request.ChargePayments).ConfigureAwait(false);
            return response;
        }
    }
}
