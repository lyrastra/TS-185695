using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToNaturalPersons)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class PaymentToNaturalPersonsProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IPaymentToNaturalPersonsReader reader;
        private readonly PaymentToNaturalPersonsEventWriter eventWriter;

        public PaymentToNaturalPersonsProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IPaymentToNaturalPersonsReader reader,
            PaymentToNaturalPersonsEventWriter eventWriter)
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
            await eventWriter.WriteProvideRequiredEventAsync(response);
        }
    }
}
