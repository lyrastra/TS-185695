using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(ITransferToAccountIgnoreNumberSaver))]
    internal sealed class TransferToAccountIgnoreNumberSaver : ITransferToAccountIgnoreNumberSaver
    {
        private readonly UpdateExistOperationsNotifier updateExistOperationsNotifier;
        private readonly IPaymentOrderApiClient paymentOrderApiClient;

        public TransferToAccountIgnoreNumberSaver(
            UpdateExistOperationsNotifier updateExistOperationsNotifier,
            IPaymentOrderApiClient paymentOrderApiClient)
        {
            this.updateExistOperationsNotifier = updateExistOperationsNotifier;
            this.paymentOrderApiClient = paymentOrderApiClient;
        }


        public async Task ApplyIgnoreNumberAsync(TransferToAccountApplyIgnoreNumberRequest applyRequest)
        {
            await paymentOrderApiClient.ApplyIgnoreNumberAsync(applyRequest.DocumentBaseIds);
            await updateExistOperationsNotifier
                .NotifyAsync(applyRequest.ImportRuleId, applyRequest.DocumentBaseIds.Length);
        }
    }
}
