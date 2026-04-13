using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Handler.Mappers
{
    internal static class LinkedDocumentsMapper
    {
        public static BillLinkSaveRequest[] MapBillLinks(IReadOnlyCollection<Kafka.Abstractions.Models.BillLink> links)
        {
            return links?.Select(link => new BillLinkSaveRequest
            {
                BillBaseId = link.BillBaseId,
                LinkSum = link.LinkSum
            }).ToArray() ?? Array.Empty<BillLinkSaveRequest>();
        }

        public static DocumentLinkSaveRequest[] MapDocumentLinks(IReadOnlyCollection<Kafka.Abstractions.Models.DocumentLink> links)
        {
            return links?.Select(link => new DocumentLinkSaveRequest
            {
                DocumentBaseId = link.DocumentBaseId,
                LinkSum = link.LinkSum
            }).ToArray() ?? Array.Empty<DocumentLinkSaveRequest>();
        }
    }
}
