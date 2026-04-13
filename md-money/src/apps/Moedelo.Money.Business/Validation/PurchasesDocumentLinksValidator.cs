using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(PurchasesDocumentLinksValidator))]
    internal class PurchasesDocumentLinksValidator
    {
        private static readonly IReadOnlyCollection<LinkedDocumentType> RestrictDocumentTypes = new[]
        {
            LinkedDocumentType.Waybill,
            LinkedDocumentType.Statement,
            LinkedDocumentType.Upd,
            LinkedDocumentType.InventoryCard,
            LinkedDocumentType.ReceiptStatement
        };
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;
        private readonly IInventoryCardApiClient inventoryCardApiClient;

        public PurchasesDocumentLinksValidator(
            IExecutionInfoContextAccessor contextAccessor,
            IBaseDocumentReader baseDocumentReader,
            LinkedDocumentPaidSumReader paidSumReader,
            IInventoryCardApiClient inventoryCardApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.baseDocumentReader = baseDocumentReader;
            this.paidSumReader = paidSumReader;
            this.inventoryCardApiClient = inventoryCardApiClient;
        }

        public async Task<BaseDocument[]> ValidateAsync(long paymentBaseId, IReadOnlyCollection<DocumentLinkSaveRequest> documentLinks)
        {
            var documentBaseIds = documentLinks.Select(x => x.DocumentBaseId).ToArray();
            await DocumentLinksDuplicateValidator.Validate(documentBaseIds);

            var baseDocuments = (await baseDocumentReader.GetByIdsAsync(documentBaseIds))
                .Where(x => RestrictDocumentTypes.Contains(x.Type))
                .ToArray();

            var baseDocumentsById = baseDocuments.ToDictionary(x => x.Id);
            var paidSums = await paidSumReader.GetPaidSumsForOutgoingPaymentAsync(paymentBaseId, baseDocuments, RestrictDocumentTypes);

            var i = 0;
            foreach (var document in documentLinks)
            {
                if (baseDocumentsById.TryGetValue(document.DocumentBaseId, out var baseDocument) == false)
                {
                    throw new BusinessValidationException($"Documents[{i}].DocumentBaseId", $"Не найден документ с базовым ид {document.DocumentBaseId}");
                }
                var documentSum = await GetBaseDocumentSum(baseDocument);
                var accessibleSum = documentSum - paidSums.GetValueOrDefault(document.DocumentBaseId, 0);
                if (accessibleSum < document.LinkSum)
                {
                    throw new BusinessValidationException($"Documents[{i}].Sum", $"Сумма документа с базовым ид {document.DocumentBaseId} доступная к оплате меньше, чем сумма указаная к оплате");
                }
                i++;
            }

            return baseDocuments;
        }

        private async Task<decimal?> GetBaseDocumentSum(BaseDocument baseDocument)
        {
            if (baseDocument.Type == LinkedDocumentType.InventoryCard)
            {
                var context = contextAccessor.ExecutionInfoContext;
                var inventoryCards = await inventoryCardApiClient.GetByBaseIdsAsync(context.FirmId, context.UserId, new[] { baseDocument.Id });
                return inventoryCards.FirstOrDefault()?.TaxDescription.Cost ?? baseDocument.Sum;
            }
            return baseDocument.Sum;
        }
    }
}
