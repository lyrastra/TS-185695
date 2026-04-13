using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(IWithdrawalFromAccountIgnoreNumberSaver))]
    internal sealed class WithdrawalFromAccountIgnoreNumberSaver : IWithdrawalFromAccountIgnoreNumberSaver
    {
        private readonly UpdateExistOperationsNotifier updateExistOperationsNotifier;
        private readonly IPaymentOrderApiClient paymentOrderApiClient;

        public WithdrawalFromAccountIgnoreNumberSaver(
            UpdateExistOperationsNotifier updateExistOperationsNotifier,
            IPaymentOrderApiClient paymentOrderApiClient)
        {
            this.updateExistOperationsNotifier = updateExistOperationsNotifier;
            this.paymentOrderApiClient = paymentOrderApiClient;
        }


        public async Task ApplyIgnoreNumberAsync(WithdrawalFromAccountApplyIgnoreNumberRequest applyRequest)
        {
            await paymentOrderApiClient.ApplyIgnoreNumberAsync(applyRequest.DocumentBaseIds);
            await updateExistOperationsNotifier
                .NotifyAsync(applyRequest.ImportRuleId, applyRequest.DocumentBaseIds.Length);
        }
    }
}
