using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [OperationType(OperationType.PaymentOrderOutgoingLoanRepayment)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class LoanRepaymentProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ILoanRepaymentReader reader;
        private readonly LoanRepaymentEventWriter eventWriter;

        public LoanRepaymentProvider(
            IClosedPeriodValidator closedPeriodValidator,
            ILoanRepaymentReader reader,
            LoanRepaymentEventWriter eventWriter)
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
