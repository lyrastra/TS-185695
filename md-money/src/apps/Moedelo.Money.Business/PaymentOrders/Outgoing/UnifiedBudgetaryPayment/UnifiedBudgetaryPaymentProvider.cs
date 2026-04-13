using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [OperationType(OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentProvider))]
    class UnifiedBudgetaryPaymentProvider : IConcretePaymentOrderProvider, IUnifiedBudgetaryPaymentProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IUnifiedBudgetaryPaymentReader reader;
        private readonly IUnifiedBudgetaryPaymentEventWriter eventWriter;

        public UnifiedBudgetaryPaymentProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IUnifiedBudgetaryPaymentReader reader,
            IUnifiedBudgetaryPaymentEventWriter eventWriter)
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
