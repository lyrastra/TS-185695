using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public interface ITaxPosting
    {
    }

    public interface IDocumentId
    {
        long DocumentId { get; set; }
    }

    public interface IRelatedDocumentBaseIds
    {
        IReadOnlyCollection<long> RelatedDocumentBaseIds { get; set; }
    }
}
