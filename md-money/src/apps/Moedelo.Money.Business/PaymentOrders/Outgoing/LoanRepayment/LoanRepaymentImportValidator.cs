using Moedelo.Contracts.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [InjectAsSingleton(typeof(LoanRepaymentImportValidator))]
    class LoanRepaymentImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;

        public LoanRepaymentImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(LoanRepaymentImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontragentAsync(request);
        }

        private async Task ValidateKontragentAsync(LoanRepaymentImportRequest request)
        {
            if (request.OperationState == OperationState.MissingKontragent)
            {
                return;
            }
            await kontragentsValidator.ValidateAsync(request.Kontragent.Id);

            if (request.OperationState == OperationState.MissingContract)
            {
                return;
            }
            var contract = await contractsValidator.ValidateAsync(request.ContractBaseId, request.Kontragent.Id);
            if (contract.ContractKind != ContractKind.ReceivedCredit &&
                contract.ContractKind != ContractKind.ReceivedLoan)
            {
                throw new BusinessValidationException(nameof(request.ContractBaseId), $"Договор с ид {request.ContractBaseId} не является договором получения займа или кредита");
            }
            if (request.LoanInterestSum.HasValue && request.LoanInterestSum.Value > request.Sum)
            {
                throw new BusinessValidationException(nameof(request.LoanInterestSum), $"Сумма процентов не может быть больше суммы операции");
            }
        }
    }
}