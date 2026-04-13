using Moedelo.Contracts.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn
{
    [InjectAsSingleton(typeof(ILoanReturnValidator))]
    class LoanReturnValidator : ILoanReturnValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;

        public LoanReturnValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            TaxPostingsValidator taxPostingsValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.taxPostingsValidator = taxPostingsValidator;
        }

        public async Task ValidateAsync(LoanReturnSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateKontragentAsync(request);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }

        private async Task ValidateKontragentAsync(LoanReturnSaveRequest request)
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
            if (contract.ContractKind != ContractKind.OutgoingLoan)
            {
                throw new BusinessValidationException(nameof(request.ContractBaseId), $"Договор с ид {request.ContractBaseId} не является договором выдачи займа");
            }
        }
    }
}