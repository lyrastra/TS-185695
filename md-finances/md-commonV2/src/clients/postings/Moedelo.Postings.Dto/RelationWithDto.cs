using System;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Postings.Dto
{
    public class RelationWithDto
    {
        public long RelatedDocumentId { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public LinkType Type { get; set; }
    }
}