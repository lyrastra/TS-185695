using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.TaxPostings
{
    public class TaxPostingsClientData
    {
        public List<TaxPostingDescriptionClientData> Postings { get; set; }

        public List<TaxPostingLinkedDocumentClientData> LinkedDocuments { get; set; }

        public bool IsManualEdit { get; set; }

        public OperationType OperationType { get; set; }

        public bool HasPostings => Postings?.Any() ?? false;
    }
}