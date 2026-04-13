using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    [InjectAsSingleton(typeof(ContributionOfOwnFundsImportValidator))]
    internal sealed class ContributionOfOwnFundsImportValidator
    {
        private readonly ILegalTypeValidator legalTypeValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;

        public ContributionOfOwnFundsImportValidator(
            ILegalTypeValidator legalTypeValidator,
            ISettlementAccountsValidator settlementAccountsValidator)
        {
            this.legalTypeValidator = legalTypeValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
        }

        public async Task ValidateAsync(ContributionOfOwnFundsImportRequest request)
        {
            await legalTypeValidator.ValidateForIpAsync().ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
        }
    }
}
