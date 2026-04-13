using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.CurrencyBankFee;
using Moedelo.Money.PaymentOrders.Business.Banks;
using Moedelo.Money.PaymentOrders.Business.SettlementAccounts;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    [InjectAsSingleton(typeof(ICurrencyBankFeeCreator))]
    class CurrencyBankFeeCreator : ICurrencyBankFeeCreator
    {
        private readonly IPaymentOrderCreator paymentOrderCreator;
        private readonly ISettlementAccountsReader settlementAccountReader;
        private readonly IBankReader bankGetter;

        public CurrencyBankFeeCreator(
            IPaymentOrderCreator paymentOrderCreator,
            ISettlementAccountsReader settlementAccountReader,
            IBankReader bankGetter)
        {
            this.paymentOrderCreator = paymentOrderCreator;
            this.settlementAccountReader = settlementAccountReader;
            this.bankGetter = bankGetter;
        }

        public async Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request)
        {
            var settlementAccount = await settlementAccountReader.GetByIdAsync(request.PaymentOrder.SettlementAccountId).ConfigureAwait(false);
            var bank = await bankGetter.GetByIdAsync(settlementAccount.BankId).ConfigureAwait(false);
            request.PaymentOrder.KontragentName = bank.FullName;
            return await paymentOrderCreator.CreateAsync(request).ConfigureAwait(false);
        }
    }
}