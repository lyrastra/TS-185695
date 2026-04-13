using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.FinancialAssistance
{
    [InjectAsSingleton(typeof(IFinancialAssistanceValidator))]
    internal sealed class FinancialAssistanceValidator : IFinancialAssistanceValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;
        private readonly IAuthorizedCapitalApiClient authorizedCapitalApiClient;

        public FinancialAssistanceValidator(
            IExecutionInfoContextAccessor contextAccessor,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            TaxPostingsValidator taxPostingsValidator,
            IAuthorizedCapitalApiClient authorizedCapitalApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.authorizedCapitalApiClient = authorizedCapitalApiClient;
        }

        public async Task ValidateAsync(FinancialAssistanceSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontagentAsync(request);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);

            if (request.TaxPostings.OsnoTaxPostings.Any(p => p.Direction == Moedelo.TaxPostings.Enums.TaxPostingDirection.Outgoing))
            {
                throw new BusinessValidationException("TaxPostings.OsnoTaxPostings", "Данный тип операции не поддерживает расход в НУ");
            }
        }
        private async Task ValidateKontagentAsync(FinancialAssistanceSaveRequest request)

        {
            if (request.OperationState == OperationState.MissingKontragent)
            {
                return;
            }

            await kontragentsValidator.ValidateAsync(request.Kontragent);

            var context = contextAccessor.ExecutionInfoContext;
            var share = await authorizedCapitalApiClient.GetShareByKontragentAsync(context.FirmId, context.UserId, request.Kontragent.Id);
            if (share <= 0)
            {
                throw new BusinessValidationException("Contractor.Id", $"Контрагент с ид {request.Kontragent.Id} не является учредителем");
            }
        }
    }
}