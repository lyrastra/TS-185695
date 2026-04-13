using System.Collections.Generic;
using System.Linq;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(PaymentFromCustomerDocumentsLinksValidator))]
    internal sealed class PaymentFromCustomerDocumentsLinksValidator
    {
        private static readonly IReadOnlyCollection<LinkedDocumentType> RestrictDocumentTypes = new[]
        {
            LinkedDocumentType.Waybill,
            LinkedDocumentType.Statement,
            LinkedDocumentType.SalesUpd
        };
        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;

        public PaymentFromCustomerDocumentsLinksValidator(
            IBaseDocumentReader baseDocumentReader, 
            LinkedDocumentPaidSumReader paidSumReader)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.paidSumReader = paidSumReader;
        }

        public async Task ValidateAsync(PaymentFromCustomerSaveRequest request)
        {
            PaymentSumCoveringValidator.Validate(request.Sum, request.DocumentLinks, request.ReserveSum);

            var documentLinks = request.DocumentLinks;
            if (documentLinks.Count == 0)
            {
                return;
            }
            
            var documentBaseIds = documentLinks.Select(x => x.DocumentBaseId).ToArray();
            await DocumentLinksDuplicateValidator.Validate(documentBaseIds);

            var baseDocuments = await baseDocumentReader.GetByIdsAsync(documentBaseIds);
            var paidSums = await paidSumReader.GetPaidSumsForIncomingPaymentAsync(request.DocumentBaseId, baseDocuments, RestrictDocumentTypes);

            DocumentLinksValidator.Validate(documentLinks, RestrictDocumentTypes, baseDocuments, paidSums);
        }
    }
}