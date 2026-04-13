using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentUpdater))]
    internal class RentPaymentUpdater : IRentPaymentUpdater
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderUpdater paymentOrderUpdater;
        private readonly IRentPeriodDao rentPeriodDao;

        public RentPaymentUpdater(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderUpdater paymentOrderUpdater,
            IRentPeriodDao rentPeriodDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderUpdater = paymentOrderUpdater;
            this.rentPeriodDao = rentPeriodDao;
        }

        public async Task UpdateAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            await paymentOrderUpdater.UpdateAsync(request);
            await rentPeriodDao.OverwriteAsync((int)context.FirmId, request.DocumentBaseId, request.RentPeriods);
        }
    }
}
