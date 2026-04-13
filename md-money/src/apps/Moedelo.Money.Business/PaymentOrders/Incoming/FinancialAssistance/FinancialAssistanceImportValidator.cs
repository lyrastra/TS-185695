using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.FinancialAssistance
{
    [InjectAsSingleton(typeof(FinancialAssistanceImportValidator))]
    internal sealed class FinancialAssistanceImportValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IAuthorizedCapitalApiClient authorizedCapitalApiClient;

        public FinancialAssistanceImportValidator(
            IExecutionInfoContextAccessor contextAccessor,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IAuthorizedCapitalApiClient authorizedCapitalApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.authorizedCapitalApiClient = authorizedCapitalApiClient;
        }

        public async Task ValidateAsync(FinancialAssistanceImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontagentAsync(request);
        }

        private async Task ValidateKontagentAsync(FinancialAssistanceImportRequest request)
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