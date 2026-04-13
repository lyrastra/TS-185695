using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Requisites;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Generators
{
    [InjectAsSingleton(typeof(MovementPaymentSnapshotGenerator))]
    internal class MovementPaymentSnapshotGenerator
    {
        private readonly FirmOrderDetailsGetter firmOrderDetailsGetter;

        public MovementPaymentSnapshotGenerator(FirmOrderDetailsGetter firmOrderDetailsGetter)
        {
            this.firmOrderDetailsGetter = firmOrderDetailsGetter;
        }

        public async Task<PaymentOrderSnapshot> GenerateAsync(PaymentOrderSaveRequest request)
        {
            var snapshot = new PaymentOrderSnapshot
            {
                PaymentNumber = request.PaymentOrder.Number,
                OrderDate = request.PaymentOrder.Date.Date,
                BankDocType = request.PaymentOrder.OrderType,
                Sum = request.PaymentOrder.Sum,
                Direction = request.PaymentOrder.Direction,
                Purpose = request.PaymentOrder.Description,
                PaymentPriority = PaymentPriority.Fifth,
            };

            if (request.PaymentOrder.Direction == MoneyDirection.Outgoing)
            {
                snapshot.Payer = await GetDetailsAsync(request.PaymentOrder.SettlementAccountId, request.PaymentOrder.OperationState);
                snapshot.Recipient = await GetDetailsAsync(request.PaymentOrder.TransferSettlementAccountId, request.PaymentOrder.OperationState);
            }
            else
            {
                snapshot.Payer = await GetDetailsAsync(request.PaymentOrder.TransferSettlementAccountId, request.PaymentOrder.OperationState);
                snapshot.Recipient = await GetDetailsAsync(request.PaymentOrder.SettlementAccountId, request.PaymentOrder.OperationState);
            }

            return snapshot;
        }

        private Task<OrderDetails> GetDetailsAsync(int? settlementAccountId, OperationState operationState)
        {
            if (settlementAccountId > 0 || operationState != OperationState.OutsourceProcessingValidationFailed)
            {
                return firmOrderDetailsGetter.GetAsync(settlementAccountId.GetValueOrDefault());
            }

            return Task.FromResult(new OrderDetails());
        }
    }
}
