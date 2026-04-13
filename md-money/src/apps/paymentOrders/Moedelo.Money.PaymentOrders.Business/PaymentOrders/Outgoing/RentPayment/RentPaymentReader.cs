using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.RentPayment;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentReader))]
    internal class RentPaymentReader : IRentPaymentReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderReader paymentOrderReader;
        private readonly IRentPeriodDao rentPeriodDao;

        public RentPaymentReader(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderReader paymentOrderReader,
            IRentPeriodDao rentPeriodDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderReader = paymentOrderReader;
            this.rentPeriodDao = rentPeriodDao;
        }

        public async Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var response = await paymentOrderReader.GetByBaseIdAsync(documentBaseId, OperationType.PaymentOrderOutgoingRentPayment);
            response.RentPeriods = await rentPeriodDao.GetAsync((int)context.FirmId, documentBaseId);
            return response;
        }
    }
}
