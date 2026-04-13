using Moedelo.Common.Enums.Enums.TaxPostings;
using System.Collections.Generic;

namespace Moedelo.KudirOsno.Client.TaxPostings.Dto
{
    public class DocumentTaxPostingsResponseDto
    {
        public IReadOnlyCollection<TaxPostingDto> Postings { get; set; }

        public IReadOnlyCollection<LinkedPaymentTaxPostingsDto> LinkedPayments { get; set; }

        public TaxPostingStatus Status { get; set; }

        public string Message { get; set; }
    }
}
