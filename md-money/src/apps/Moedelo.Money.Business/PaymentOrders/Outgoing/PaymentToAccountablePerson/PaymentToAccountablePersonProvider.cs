using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToAccountablePerson)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class PaymentToAccountablePersonProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IPaymentToAccountablePersonReader reader;
        private readonly PaymentToAccountablePersonEventWriter eventWriter;

        public PaymentToAccountablePersonProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IPaymentToAccountablePersonReader reader,
            PaymentToAccountablePersonEventWriter eventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.reader = reader;
            this.eventWriter = eventWriter;
        }

        public async Task ProvideAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (response.OperationState.IsBadOperationState())
            {
                return;
            }
            await closedPeriodValidator.ValidateAsync(response.Date);
            // todo: вероятно, если платеж привязан к АО, то нужно вызывать перепроведение АО
            await eventWriter.WriteProvideRequiredEventAsync(response);
        }
    }
}
