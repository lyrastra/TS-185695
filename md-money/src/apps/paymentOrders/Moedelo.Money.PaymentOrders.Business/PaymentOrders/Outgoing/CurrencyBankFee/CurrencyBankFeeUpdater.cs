using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.CurrencyBankFee;
using Moedelo.Money.PaymentOrders.Business.Banks;
using Moedelo.Money.PaymentOrders.Business.SettlementAccounts;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    [InjectAsSingleton(typeof(ICurrencyBankFeeUpdater))]
    class CurrencyBankFeeUpdater : ICurrencyBankFeeUpdater
    {
        private readonly IPaymentOrderUpdater paymentOrderUpdater;
        private readonly ISettlementAccountsReader settlementAccountReader;
        private readonly IBankReader bankGetter;

        public CurrencyBankFeeUpdater(
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