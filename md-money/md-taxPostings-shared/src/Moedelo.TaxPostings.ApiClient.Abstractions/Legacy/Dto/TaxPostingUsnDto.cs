using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto
{
    public class TaxPostingUsnDto
    {
        public long Id { get; set; }

        public DateTime PostingDate { get; set; }

        public decimal Sum { get; set; }

        public string NumberOfDocument { get; set; }

        public string Destination { get; set; }

        public TaxPostingDirection Direction { get; set; }

        public long? DocumentId { get; set; }

        public DateTime? DocumentDate { get; set; }

        public List<long> RelatedDocumentBaseIds { get; set; }
    }
}
