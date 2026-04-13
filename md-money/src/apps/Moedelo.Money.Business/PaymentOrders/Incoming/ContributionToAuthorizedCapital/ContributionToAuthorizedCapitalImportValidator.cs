using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton(typeof(ContributionToAuthorizedCapitalImportValidator))]
    internal sealed class ContributionToAuthorizedCapitalImportValidator
    {
        private readonly ILegalTypeValidator legalTypeValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;

        public ContributionToAuthorizedCapitalImportValidator(
            ILegalTypeValidator legalTypeValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator)
        {
            this.legalTypeValidator = legalTypeValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
        }

        public async Task ValidateAsync(ContributionToAuthorizedCapitalImportRequest request)
        {
            await legalTypeValidator.ValidateForUlAsync();
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Kontragent);
            }
        }
    }
}