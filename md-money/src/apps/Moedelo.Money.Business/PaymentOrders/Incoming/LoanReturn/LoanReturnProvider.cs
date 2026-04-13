using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn
{
    [OperationType(OperationType.PaymentOrderIncomingLoanReturn)]
    [InjectAsSingleton(typeof(ILoanReturnProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class LoanReturnProvider : ILoanReturnProvider, IConcretePaymentOrderProvider
    {
        private readonly ILoanReturnReader reader;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly LoanReturnEventWriter eventWriter;

        public LoanReturnProvider(
            ILoanReturnReader reader,
            IClosedPeriodValidator closedPeriodValidator,
            LoanReturnEventWriter eventWriter)
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
