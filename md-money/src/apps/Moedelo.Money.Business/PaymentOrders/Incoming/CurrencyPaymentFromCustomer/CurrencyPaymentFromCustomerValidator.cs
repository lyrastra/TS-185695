using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerValidator))]
    internal sealed class CurrencyPaymentFromCustomerValidator : ICurrencyPaymentFromCustomerValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly CurrencyPaymentFromCustomerDocumentsValidator documentsValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;

        public CurrencyPaymentFromCustomerValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            CurrencyPaymentFromCustomerDocumentsValidator documentsValidator,
            ICurrencyOperationsAccessValidator accessValidator,
            TaxPostingsValidator taxPostingsValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.documentsValidator = documentsValidator;
            this.accessValidator = accessValidator;
            this.taxPostingsValidator = taxPostingsValidator;
        }

        public async Task ValidateAsync(CurrencyPaymentFromCustomerSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateCurrencyAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Kontragent);
                if (request.ContractBaseId.HasValue)
                {
                    await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
                }
            }
            ValidatePaymentSum(request);
            await documentsValidator.ValidateAsync(request);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }

        private static void ValidatePaymentSum(CurrencyPaymentFromCustomerSaveRequest request)
        {
            if (request.LinkedDocuments?.Any() != true)
            {
                return;
            }

            var linksSum = request.LinkedDocuments?.Sum(ld => ld.LinkSum);

            if (request.Sum < linksSum)
            {
                throw new BusinessValidationException("Sum", $"Сумма оплаты по документам ({linksSum}) превышает сумму платежного поручения ({request.Sum})");
            }
        }
    }
}