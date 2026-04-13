using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton(typeof(IRefundToCustomerValidator))]
    internal sealed class RefundToCustomerValidator : IRefundToCustomerValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly NumberValidator numberValidator;
        private readonly RefundToCustomerTaxPostingsValidator taxPostingsValidator;
        private readonly IPaymentOrderGetter paymentOrderGetter;

        public RefundToCustomerValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            ITaxationSystemValidator taxationSystemValidator,
            IContractsValidator contractsValidator,
            NumberValidator numberValidator,
            RefundToCustomerTaxPostingsValidator taxPostingsValidator,
            IPaymentOrderGetter paymentOrderGetter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.contractsValidator = contractsValidator;
            this.numberValidator = numberValidator;
            this.paymentOrderGetter = paymentOrderGetter;
            this.taxPostingsValidator = taxPostingsValidator;
        }

        public async Task ValidateAsync(RefundToCustomerSaveRequest request)
        {
            await ValidatePaymentNumber(request);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await ValidateContractorAsync(request);
            if (request.TaxationSystemType.HasValue)
            {
                await taxationSystemValidator.ValidateAsync(request.Date, request.TaxationSystemType.Value);
            }
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }

        private async Task ValidateContractorAsync(RefundToCustomerSaveRequest request)
        {
            if (request.OperationState == OperationState.MissingKontragent)
            {
                return;
            }
            await kontragentsValidator.ValidateAsync(request.Kontragent);
            if (request.ContractBaseId.HasValue)
            {
                await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
            }
        }

        private async Task ValidatePaymentNumber(RefundToCustomerSaveRequest request)
        {
            if (request.DocumentBaseId == 0)
            {
                await numberValidator.ValidatePaymentOrderAsync(false, request.Number);
            }
            else
            {
                var response = await paymentOrderGetter.GetIsFromImportAsync(request.DocumentBaseId);
                await numberValidator.ValidatePaymentOrderAsync(response.IsFromImport, request.Number);
            }
        }
    }
}