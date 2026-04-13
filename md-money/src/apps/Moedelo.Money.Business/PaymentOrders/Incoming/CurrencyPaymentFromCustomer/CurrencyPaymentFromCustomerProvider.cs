using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer)]
    [InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class CurrencyPaymentFromCustomerProvider : ICurrencyPaymentFromCustomerProvider, IConcretePaymentOrderProvider
    {
        //private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ICurrencyPaymentFromCustomerReader reader;
        private readonly CurrencyPaymentFromCustomerEventWriter eventWriter;

        public CurrencyPaymentFromCustomerProvider(
            //IClosedPeriodValidator closedPeriodValidator,
            ICurrencyPaymentFromCustomerReader reader,
            CurrencyPaymentFromCustomerEventWriter eventWriter)
        {
            //this.closedPeriodValidator = closedPeriodValidator;
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
            // костыль, так как не получается перепровести первичку в отрыве от п/п
            //await closedPeriodValidator.ValidateAsync(response.Date);
            await eventWriter.WriteProvideRequiredEventAsync(response);
        }
    }
}