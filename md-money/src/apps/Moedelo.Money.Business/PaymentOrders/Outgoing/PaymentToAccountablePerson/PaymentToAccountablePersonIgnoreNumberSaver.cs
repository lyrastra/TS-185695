using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.Operations;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonIgnoreNumberSaver))]
    internal sealed class PaymentToAccountablePersonIgnoreNumberSaver : IPaymentToAccountablePersonIgnoreNumberSaver
    {
        private readonly UpdateExistOperationsNotifier updateExistOperationsNotifier;
        private readonly IPaymentOrderApiClient paymentOrderApiClient;

        public PaymentToAccountablePersonIgnoreNumberSaver(
            UpdateExistOperationsNotifier updateExistOperationsNotifier,
            IPaymentOrderApiClient paymentOrderApiClient)
        {
            this.updateExistOperationsNotifier = updateExistOperationsNotifier;
            this.paymentOrderApiClient = paymentOrderApiClient;
        }

        public async Task ApplyIgnoreNumberAsync(PaymentToAccountablePersonApplyIgnoreNumberRequest applyRequest)
        {
            await paymentOrderApiClient.ApplyIgnoreNumberAsync(applyRequest.DocumentBaseIds);
            await updateExistOperationsNotifier
                .NotifyAsync(applyRequest.ImportRuleId, applyRequest.DocumentBaseIds.Length);
        }
    }
}
