using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierValidator))]
    internal sealed class CurrencyPaymentToSupplierValidator : ICurrencyPaymentToSupplierValidator
    {
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;
        private readonly CurrencyPaymentToSupplierDocumentsValidator documentsValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public CurrencyPaymentToSupplierValidator(
            ITaxationSystemValidator taxationSystemValidator,
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            ICurrencyOperationsAccessValidator accessValidator,
            CurrencyPaymentToSupplierDocumentsValidator documentsValidator,
            TaxPostingsValidator taxPostingsValidator,
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.taxationSystemValidator = taxationSystemValidator;
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.accessValidator = accessValidator;
            this.documentsValidator = documentsValidator;
            this.taxPostingsValidator = taxPostingsValidator;
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task ValidateAsync(CurrencyPaymentToSupplierSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year);
            await closedPeriodValidator.ValidateAsync(request.Date);
            var settlementAccount = await settlementAccountsValidator.ValidateCurrencyAsync(request.SettlementAccountId);
            await kontragentsValidator.ValidateAsync(request.Kontragent);

            if (request.ContractBaseId.HasValue)
            {
                await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
            }

            var isOoo = await firmRequisitesReader.IsOooAsync();
            if (!isOoo)
            {
                await documentsValidator.ValidateAsync(request, settlementAccount);
            }
            else if (request.DocumentLinks?.Count > 0)
            {
                throw new BusinessValidationException($"Documents", $"Для ООО не поддерживается прикрепление документов");
            }

            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }
    }
}