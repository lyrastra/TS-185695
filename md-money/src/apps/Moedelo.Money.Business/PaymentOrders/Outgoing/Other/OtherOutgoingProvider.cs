using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;
using Moedelo.Money.Enums.Extensions;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [OperationType(OperationType.PaymentOrderOutgoingOther)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class OtherOutgoingProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IOtherOutgoingReader reader;
        private readonly OtherOutgoingEventWriter eventWriter;

        public OtherOutgoingProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IOtherOutgoingReader reader,
            OtherOutgoingEventWriter eventWriter)
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
