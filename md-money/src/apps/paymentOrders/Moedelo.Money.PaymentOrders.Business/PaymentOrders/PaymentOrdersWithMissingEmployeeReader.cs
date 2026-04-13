using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrdersWithMissingEmployeeReader))]
    public class PaymentOrdersWithMissingEmployeeReader : IPaymentOrdersWithMissingEmployeeReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderDao paymentOrderDao;

        public PaymentOrdersWithMissingEmployeeReader(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderDao paymentOrderDao)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderDao = paymentOrderDao;
        }

        public async Task<IReadOnlyCollection<PaymentOrderWithMissingEmployeeResponse>> GetAsync()
        {
            var firmId = (int) contextAccessor.ExecutionInfoContext.FirmId;

            var paymentOrders = await paymentOrderDao.GetByOperationStateAsync(firmId, OperationState.MissingWorker);
            return paymentOrders
                .Select(paymentOrder =>
                {
                    var snapshot = XmlHelper.Deserialize<PaymentOrderSnapshot>(paymentOrder.PaymentSnapshot);
                    return new PaymentOrderWithMissingEmployeeResponse
                    {
                        DocumentBaseId = paymentOrder.DocumentBaseId,
                        OperationType = paymentOrder.OperationType,
                        WorkerInn = snapshot.Recipient.Inn,
                        WorkerName = snapshot.Recipient.Name,
                        WorkerSettlementAccount = snapshot.Recipient.SettlementNumber
                    };
                }).ToArray();
        }
    }
}