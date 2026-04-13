using System.Collections.Generic;
using System.Linq;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using System.Threading.Tasks;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee
{
    [InjectAsSingleton(typeof(MediationFeeDocumentsLinksValidator))]
    internal sealed class MediationFeeDocumentsLinksValidator
    {
        private static readonly IReadOnlyCollection<LinkedDocumentType> RestrictDocumentTypes = new[]
        {
            LinkedDocumentType.Waybill,
            LinkedDocumentType.Statement,
            LinkedDocumentType.MiddlemanReport
        };
        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;

        public MediationFeeDocumentsLinksValidator(
            IBaseDocumentReader baseDocumentReader, 
            LinkedDocumentPaidSumReader paidSumReader)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.paidSumReader = paidSumReader;
        }

        public async Task ValidateAsync(MediationFeeSaveRequest request)
        {
            PaymentSumCoveringValidator.Validate(request.Sum, request.DocumentLinks);

            var documentLinks = request.DocumentLinks;
            if (documentLinks.Count == 0)
            {
                return;
            }
            
            var documentBaseIds = documentLinks.Select(x => x.DocumentBaseId).ToArray();
            await DocumentLinksDuplicateValidator.Validate(documentBaseIds);

            var baseDocuments = await baseDocumentReader.GetByIdsAsync(documentBaseIds);
            var paidSums = await paidSumReader.GetPaidSumsForMediationFeeAsync(request.DocumentBaseId, documentBaseIds, RestrictDocumentTypes);

            DocumentLinksValidator.Validate(documentLinks, RestrictDocumentTypes, baseDocuments, paidSums);
        }
    }
}