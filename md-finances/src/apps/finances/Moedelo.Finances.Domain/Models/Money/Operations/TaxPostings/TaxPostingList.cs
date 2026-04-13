using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Finances.Domain.Models.Money.Operations.TaxPostings
{
    public class TaxPostingList
    {
        public bool IsManual { get; set; }
        public string Message { get; set; }
        public OperationType OperationType { get; set; }
        public IReadOnlyCollection<TaxPosting> Postings { get; set; } = new List<TaxPosting>();
        public IReadOnlyCollection<TaxPostingLinkedDocument> LinkedDocuments { get; set; } = new List<TaxPostingLinkedDocument>();
    }
}
