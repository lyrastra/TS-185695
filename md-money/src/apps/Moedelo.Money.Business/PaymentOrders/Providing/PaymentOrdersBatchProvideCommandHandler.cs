using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Providing
{
    [InjectAsSingleton(typeof(IPaymentOrdersBatchProvideCommandHandler))]
    internal sealed class PaymentOrdersBatchProvideCommandHandler: IPaymentOrdersBatchProvideCommandHandler
    {
        private readonly IPaymentOrderProvider paymentOrderProvider;
        private readonly PaymentOrderProvidingStateSetter providingStateSetter;

        public PaymentOrdersBatchProvideCommandHandler(
            IPaymentOrderProvider paymentOrderProvider,
            PaymentOrderProvidingStateSetter providingStateSetter)
        {
            this.paymentOrderProvider = paymentOrderProvider;
            this.providingStateSetter = providingStateSetter;
        }

        public async Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds, long batchProvidingStateId)
        {
            await paymentOrderProvider.ProvideAsync(documentBaseIds);

            if (batchProvidingStateId > 0)
            {
                await providingStateSetter.UnsetStateAsync(batchProvidingStateId);
            }
        }
    }
}
