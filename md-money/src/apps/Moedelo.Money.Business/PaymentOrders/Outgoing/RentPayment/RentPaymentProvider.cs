using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [OperationType(OperationType.PaymentOrderOutgoingRentPayment)]
    [InjectAsSingleton(typeof(IRentPaymentProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class RentPaymentProvider : IRentPaymentProvider, IConcretePaymentOrderProvider
    {
        private readonly IRentPaymentReader reader;
        private readonly RentPaymentEventWriter commandWriter;

        public RentPaymentProvider(
            IRentPaymentReader reader,
            RentPaymentEventWriter commandWriter)
        {
            this.reader = reader;
            this.commandWriter = commandWriter;
        }

        public async Task ProvideAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (response.OperationState.IsBadOperationState())
            {
                return;
            }

            var command = RentPaymentMapper.Map(response);
            await commandWriter.WriteUpdatedEventAsync(command);
        }
    }
}