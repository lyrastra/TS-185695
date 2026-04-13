using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BankFee;
using Moedelo.Money.PaymentOrders.Business.Banks;
using Moedelo.Money.PaymentOrders.Business.SettlementAccounts;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.BankFee
{
    [InjectAsSingleton(typeof(IBankFeeUpdater))]
    internal class BankFeeUpdater : IBankFeeUpdater
    {
        private readonly IPaymentOrderUpdater paymentOrderUpdater;
        private readonly ISettlementAccountsReader settlementAccountReader;
        private readonly IBankReader bankGetter;

        public BankFeeUpdater(
            IPaymentOrderUpdater paymentOrderUpdater,
            ISettlementAccountsReader settlementAccountReader,
            IBankReader bankGetter)
        {
            this.paymentOrderUpdater = paymentOrderUpdater;
            this.settlementAccountReader = settlementAccountReader;
            this.bankGetter = bankGetter;
        }

        public async Task UpdateAsync(PaymentOrderSaveRequest request)
        {
            var settlementAccount = await settlementAccountReader.GetByIdAsync(request.PaymentOrder.SettlementAccountId).ConfigureAwait(false);
            var bank = await bankGetter.GetByIdAsync(settlementAccount.BankId).ConfigureAwait(false);
            request.PaymentOrder.KontragentName = bank.FullName;
            await paymentOrderUpdater.UpdateAsync(request).ConfigureAwait(false);
        }
    }
}
