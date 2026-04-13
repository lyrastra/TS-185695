using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Requisites;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots.Generators
{
    [InjectAsSingleton(typeof(DefaultPaymentSnapshotGenerator))]
    internal class DefaultPaymentSnapshotGenerator
    {
        private readonly FirmOrderDetailsGetter firmOrderDetailsGetter;
        private readonly SnapshotFixer snapshotFixer;

        public DefaultPaymentSnapshotGenerator(FirmOrderDetailsGetter firmOrderDetailsGetter, SnapshotFixer snapshotFixer)
        {
            this.firmOrderDetailsGetter = firmOrderDetailsGetter;
            this.snapshotFixer = snapshotFixer;
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
                Recipient = await firmOrderDetailsGetter.GetAsync(request.PaymentOrder.SettlementAccountId).ConfigureAwait(false)
            };

            if (request.PaymentOrder.Direction == MoneyDirection.Outgoing)
            {
                snapshot.Payer = await firmOrderDetailsGetter.GetAsync(request.PaymentOrder.SettlementAccountId).ConfigureAwait(false);
                snapshot.Recipient = KontragentRequisitesMapper.Map(request.KontragentRequisites);
                await snapshotFixer.FixBankInfoAsync(snapshot.Recipient, request).ConfigureAwait(false);
            }
            else
            {
                snapshot.Payer = KontragentRequisitesMapper.Map(request.KontragentRequisites);
                await snapshotFixer.FixBankInfoAsync(snapshot.Payer, request).ConfigureAwait(false);
                snapshot.Recipient = await firmOrderDetailsGetter.GetAsync(request.PaymentOrder.SettlementAccountId).ConfigureAwait(false);
            }

            return snapshot;
        }
    }
}
