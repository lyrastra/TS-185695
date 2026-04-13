using Moedelo.Contracts.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanObtaining
{
    [InjectAsSingleton(typeof(LoanObtainingImportValidator))]
    class LoanObtainingImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;

        public LoanObtainingImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(LoanObtainingImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontragentAsync(request);
        }

        private async Task ValidateKontragentAsync(LoanObtainingImportRequest request)
        {
            if (request.OperationState == OperationState.MissingKontragent)
            {
                return;
            }
            await kontragentsValidator.ValidateAsync(request.Kontragent);

            if (request.OperationState == OperationState.MissingContract)
            {
                return;
            }
            var contract = await contractsValidator.ValidateAsync(request.ContractBaseId, request.Kontragent.Id);
            if (contract.ContractKind != ContractKind.ReceivedCredit &&
                contract.ContractKind != ContractKind.ReceivedLoan &&
                contract.IsMainContract == false)
            {
                throw new BusinessValidationException(nameof(request.ContractBaseId), $"Договор с ид {request.ContractBaseId} не является основным договором или договором получения займа или кредита");
            }
        }
    }
}