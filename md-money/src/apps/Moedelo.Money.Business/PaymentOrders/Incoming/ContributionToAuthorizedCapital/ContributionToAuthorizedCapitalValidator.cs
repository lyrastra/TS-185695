using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton(typeof(IContributionToAuthorizedCapitalValidator))]
    internal sealed class ContributionToAuthorizedCapitalValidator : IContributionToAuthorizedCapitalValidator
    {
        private readonly ILegalTypeValidator legalTypeValidator;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;

        public ContributionToAuthorizedCapitalValidator(
            ILegalTypeValidator legalTypeValidator,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator)
        {
            this.legalTypeValidator = legalTypeValidator;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
        }

        public async Task ValidateAsync(ContributionToAuthorizedCapitalSaveRequest request)
        {
            await legalTypeValidator.ValidateForUlAsync();
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Kontragent);
            }
        }
    }
}