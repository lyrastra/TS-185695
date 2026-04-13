using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(CurrencyPaymentToSupplierImportValidator))]
    internal sealed class CurrencyPaymentToSupplierImportValidator
    {
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public CurrencyPaymentToSupplierImportValidator(
            ITaxationSystemValidator taxationSystemValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.taxationSystemValidator = taxationSystemValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.accessValidator = accessValidator;
        }

        public async Task ValidateAsync(CurrencyPaymentToSupplierImportRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year);
            await settlementAccountsValidator.ValidateCurrencyAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Kontragent.Id);
                if (request.ContractBaseId.HasValue)
                {
                    await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
                }
            }
        }
    }
}