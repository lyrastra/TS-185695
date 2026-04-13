using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentCreator))]
    internal class RentPaymentCreator : IRentPaymentCreator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderCreator paymentOrderCreator;
        private readonly IRentPeriodDao rentPeriodDao;

        public RentPaymentCreator(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderCreator paymentOrderCreator,
            IRentPeriodDao rentPeriodDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderCreator = paymentOrderCreator;
            this.rentPeriodDao = rentPeriodDao;
        }

        public async Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var response = await paymentOrderCreator.CreateAsync(request);
            await rentPeriodDao.InsertAsync((int)context.FirmId, request.DocumentBaseId, request.RentPeriods);
            return response;
        }
    }
}
