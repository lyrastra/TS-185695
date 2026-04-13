using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(PaymentToSupplierImportValidator))]
    internal sealed class PaymentToSupplierImportValidator
    {
        private readonly NumberValidator numberValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;
        private readonly IContractsValidator contractsValidator;
        private readonly PurchasesDocumentLinksValidator purchasesDocumentLinksValidator;

        public PaymentToSupplierImportValidator(
            NumberValidator numberValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator,
            IContractsValidator contractsValidator,
            PurchasesDocumentLinksValidator purchasesDocumentLinksValidator)
        {
            this.numberValidator = numberValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
            this.contractsValidator = contractsValidator;
            this.purchasesDocumentLinksValidator = purchasesDocumentLinksValidator;
        }

        public async Task ValidateAsync(PaymentToSupplierImportRequest request)
        {
            await numberValidator.ValidateAsync(request.Number);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Kontragent.Id);
                if (request.ContractBaseId.HasValue)
                {
                    await contractsValidator.ValidateAsync(request.ContractBaseId.Value, request.Kontragent.Id);
                }
            }
            var documentLinksId = request.DocumentLinks?.Select(l => l.DocumentBaseId).ToList();
            await DocumentLinksDuplicateValidator.Validate(documentLinksId);
        }
    }
}