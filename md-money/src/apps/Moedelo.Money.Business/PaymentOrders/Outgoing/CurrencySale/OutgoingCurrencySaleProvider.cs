using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencySale)]
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class OutgoingCurrencySaleProvider : IOutgoingCurrencySaleProvider, IConcretePaymentOrderProvider
    {
        private readonly IOutgoingCurrencySaleReader reader;
        private readonly OutgoingCurrencySaleEventWriter eventWriter;
        private readonly IClosedPeriodValidator closedPeriodValidator;

        public OutgoingCurrencySaleProvider(
            IOutgoingCurrencySaleReader reader,
            OutgoingCurrencySaleEventWriter eventWriter,
            IClosedPeriodValidator closedPeriodValidator)
        {
            this.reader = reader;
            this.eventWriter = eventWriter;
            this.closedPeriodValidator = closedPeriodValidator;
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