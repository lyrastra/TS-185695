using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue
{
    [OperationType(OperationType.PaymentOrderOutgoingLoanIssue)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class LoanIssueProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ILoanIssueReader reader;
        private readonly LoanIssueEventWriter eventWriter;

        public LoanIssueProvider(
            IClosedPeriodValidator closedPeriodValidator,
            ILoanIssueReader reader,
            LoanIssueEventWriter eventWriter)
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
