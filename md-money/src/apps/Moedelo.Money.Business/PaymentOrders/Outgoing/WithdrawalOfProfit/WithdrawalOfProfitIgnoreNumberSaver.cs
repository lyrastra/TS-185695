using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [InjectAsSingleton(typeof(IWithdrawalOfProfitIgnoreNumberSaver))]
    internal sealed class WithdrawalOfProfitIgnoreNumberSaver : IWithdrawalOfProfitIgnoreNumberSaver
    {
        private readonly UpdateExistOperationsNotifier updateExistOperationsNotifier;
        private readonly IPaymentOrderApiClient paymentOrderApiClient;

        public WithdrawalOfProfitIgnoreNumberSaver(
            UpdateExistOperationsNotifier updateExistOperationsNotifier,
            IPaymentOrderApiClient paymentOrderApiClient)
        {
            this.updateExistOperationsNotifier = updateExistOperationsNotifier;
            this.paymentOrderApiClient = paymentOrderApiClient;
        }


        public async Task ApplyIgnoreNumberAsync(WithdrawalOfProfitApplyIgnoreNumberRequest applyRequest)
        {
            await paymentOrderApiClient.ApplyIgnoreNumberAsync(applyRequest.DocumentBaseIds);
            await updateExistOperationsNotifier
                .NotifyAsync(applyRequest.ImportRuleId, applyRequest.DocumentBaseIds.Length);
        }
    }
}
