using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyPurchase)]
    [InjectAsSingleton(typeof(IOutgoingCurrencyPurchaseProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class OutgoingCurrencyPurchaseProvider : IOutgoingCurrencyPurchaseProvider, IConcretePaymentOrderProvider
    {
        private readonly IOutgoingCurrencyPurchaseReader reader;
        private readonly OutgoingCurrencyPurchaseEventWriter eventWriter;
        private readonly IClosedPeriodValidator closedPeriodValidator;

        public OutgoingCurrencyPurchaseProvider(
            IOutgoingCurrencyPurchaseReader reader,
            OutgoingCurrencyPurchaseEventWriter eventWriter,
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