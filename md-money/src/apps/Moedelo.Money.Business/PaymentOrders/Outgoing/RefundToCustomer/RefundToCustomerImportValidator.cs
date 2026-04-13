using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton(typeof(RefundToCustomerImportValidator))]
    internal sealed class RefundToCustomerImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly IContractsValidator contractsValidator;

        public RefundToCustomerImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            ITaxationSystemValidator taxationSystemValidator,
            IContractsValidator contractsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.contractsValidator = contractsValidator;
        }

        public async Task ValidateAsync(RefundToCustomerImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateContractorAsync(request);
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value);
            }
        }

        private async Task ValidateContractorAsync(RefundToCustomerImportRequest request)
        {
            if (request.OperationState == OperationState.MissingKontragent)
            {
                return;
            }
            await kontragentsValidator.ValidateAsync(request.Kontragent.Id).ConfigureAwait(false);
            if (request.ContractBaseId.HasValue)
            {
                await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id).ConfigureAwait(false);
            }
        }
    }
}