using System;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos
{
    public class LinkWithDto
    {
        public long RelatedDocumentId { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public LinkType Type { get; set; }
    }
}