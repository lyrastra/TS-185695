using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    [InjectAsSingleton(typeof(IContributionOfOwnFundsValidator))]
    internal sealed class ContributionOfOwnFundsValidator : IContributionOfOwnFundsValidator
    {
        private readonly ILegalTypeValidator legalTypeValidator;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;

        public ContributionOfOwnFundsValidator(
            ILegalTypeValidator legalTypeValidator,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator)
        {
            this.legalTypeValidator = legalTypeValidator;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
        }

        public async Task ValidateAsync(ContributionOfOwnFundsSaveRequest request)
        {
            await legalTypeValidator.ValidateForIpAsync().ConfigureAwait(false);
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
        }
    }
}
