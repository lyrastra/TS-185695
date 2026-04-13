using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Validation
{
    internal static class DocumentLinksValidator
    {
        public static void Validate(
            IReadOnlyCollection<DocumentLinkSaveRequest> documentLinks, 
            IReadOnlyCollection<LinkedDocumentType> restrictDocumentTypes,
            BaseDocument[] baseDocuments, 
            IReadOnlyDictionary<long, decimal> paidSums)
        {
            var i = 0;
            foreach (var document in documentLinks)
            {
                var baseDocument = baseDocuments.FirstOrDefault(x => x.Id == document.DocumentBaseId);
                if (baseDocument == null)
                {
                    throw new BusinessValidationException($"Documents[{i}].DocumentBaseId",
                        $"Не найден документ с DocumentBaseId={document.DocumentBaseId}");
                }

                if (!restrictDocumentTypes.Contains(baseDocument.Type))
                {
                    throw new BusinessValidationException($"Documents[{i}]",
                        $"Документ с типом {baseDocument.Type} нельзя использовать в п/п (DocumentBaseId={document.DocumentBaseId})");
                }

                paidSums.TryGetValue(document.DocumentBaseId, out var paidSum);
                var accessibleSum = baseDocument.Sum - paidSum;
                if (accessibleSum < document.LinkSum)
                {
                    throw new BusinessValidationException($"Documents[{i}].Sum",
                        $"Сумма документа с DocumentBaseId={document.DocumentBaseId} доступная к оплате меньше, чем сумма указаная к оплате");
                }

                i++;
            }
        }
    }
}