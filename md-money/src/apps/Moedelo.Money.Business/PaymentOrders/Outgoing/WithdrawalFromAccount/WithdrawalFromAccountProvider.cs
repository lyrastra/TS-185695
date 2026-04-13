using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [OperationType(OperationType.MemorialWarrantWithdrawalFromAccount)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class WithdrawalFromAccountProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IWithdrawalFromAccountReader reader;
        private readonly WithdrawalFromAccountEventWriter eventWriter;

        public WithdrawalFromAccountProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IWithdrawalFromAccountReader reader,
            WithdrawalFromAccountEventWriter eventWriter)
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
