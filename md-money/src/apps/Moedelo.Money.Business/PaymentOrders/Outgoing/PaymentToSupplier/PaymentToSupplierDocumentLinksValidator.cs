using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(PaymentToSupplierDocumentLinksValidator))]
    internal class PaymentToSupplierDocumentLinksValidator
    {
        private readonly PurchasesDocumentLinksValidator documentLinksValidator;

        public PaymentToSupplierDocumentLinksValidator(
            PurchasesDocumentLinksValidator documentLinksValidator)
        {
            this.documentLinksValidator = documentLinksValidator;
        }

        /// <summary>
        /// Валидирует ссылки на документы по рублевым операциям
        /// </summary>
        public async Task ValidateAsync(PaymentToSupplierSaveRequest request)
        {
            PaymentSumCoveringValidator.Validate(request.Sum, request.DocumentLinks, request.ReserveSum);

            var baseDocuments = await documentLinksValidator.ValidateAsync(request.DocumentBaseId, request.DocumentLinks);
            if (request.IsMainContractor &&
                baseDocuments.Any(x => x.Type == LinkedDocumentType.ReceiptStatement))
            {
                throw new BusinessValidationException("Documents", "Акт приема-передачи недоступен для основного контрагента");
            }
        }
    }
}
