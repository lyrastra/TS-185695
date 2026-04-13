using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;
using Moedelo.Money.Enums.Extensions;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [OperationType(OperationType.PaymentOrderIncomingRefundFromAccountablePerson)]
    [InjectAsSingleton(typeof(RefundFromAccountablePersonProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class RefundFromAccountablePersonProvider : IConcretePaymentOrderProvider
    {
        private readonly IRefundFromAccountablePersonReader reader;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly RefundFromAccountablePersonEventWriter eventWriter;

        public RefundFromAccountablePersonProvider(
            IRefundFromAccountablePersonReader reader,
            IClosedPeriodValidator closedPeriodValidator,
            RefundFromAccountablePersonEventWriter eventWriter)
        {
            this.reader = reader;
            this.closedPeriodValidator = closedPeriodValidator;
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
