using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.PaymentOrders.ApiClient;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierIgnoreNumberSaver))]
    internal sealed class PaymentToSupplierIgnoreNumberSaver : IPaymentToSupplierIgnoreNumberSaver
    {
        private readonly UpdateExistOperationsNotifier updateExistOperationsNotifier;
        private readonly IPaymentOrderApiClient paymentOrderApiClient;

        public PaymentToSupplierIgnoreNumberSaver(
            UpdateExistOperationsNotifier updateExistOperationsNotifier,
            IPaymentOrderApiClient paymentOrderApiClient)
        {
            this.updateExistOperationsNotifier = updateExistOperationsNotifier;
            this.paymentOrderApiClient = paymentOrderApiClient;
        }

        public async Task ApplyIgnoreNumberAsync(PaymentToSupplierApplyIgnoreNumberRequest applyRequest)
        {
            await paymentOrderApiClient.ApplyIgnoreNumberAsync(applyRequest.DocumentBaseIds);
            await updateExistOperationsNotifier
                .NotifyAsync(applyRequest.ImportRuleId, applyRequest.DocumentBaseIds.Length);
        }
    }
}
