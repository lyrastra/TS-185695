using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Public.ClientData.Money.Operations;

namespace Moedelo.Finances.Public.Mappers.Money
{
    public static class MoneyOperationLinkedDocumentsMapper
    {
        public static List<LinkedDocumentClientData> MapLinkedDocuments(IReadOnlyCollection<LinkedDocument> linkedDocuments)
        {
            return linkedDocuments.Select(MapLinkedDocument).ToList();
        }

        private static LinkedDocumentClientData MapLinkedDocument(LinkedDocument linkedDocument)
        {
            return new LinkedDocumentClientData
            {
                Id = linkedDocument.Id,
                Date = linkedDocument.Date,
                Number = linkedDocument.Number,
                Type = linkedDocument.Type
            };
        }
    }
}
