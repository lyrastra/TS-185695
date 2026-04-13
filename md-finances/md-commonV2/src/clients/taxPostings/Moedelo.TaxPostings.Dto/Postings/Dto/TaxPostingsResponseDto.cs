using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.TaxPostings;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Dto.Postings.Dto
{
    public class TaxPostingsResponseDto<T> : ITaxPostingsResponseDto<T>
        where T : ITaxPostingDto
    {
        public IReadOnlyCollection<ILinkedDocumentTaxPostingsDto<T>> LinkedDocuments { get; set; }

        public IReadOnlyCollection<T> Postings { get; set; }

        public TaxPostingStatus TaxStatus { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public string ExplainingMessage { get; set; }

    }

    public class UsnTaxPostingsResponseDto : TaxPostingsResponseDto<UsnTaxPostingDto>
    {
    }

    public class OsnoTaxPostingsResponseDto : TaxPostingsResponseDto<OsnoTaxPostingDto>
    {
    }

    public class PatentTaxPostingsResponseDto : TaxPostingsResponseDto<PatentTaxPostingDto>
    {
    }

    public class TaxPostingsResponseDto : TaxPostingsResponseDto<ITaxPostingDto>
    {
        protected TaxPostingsResponseDto()
        {
        }

        public static TaxPostingsResponseDto NoTax =>
            new TaxPostingsResponseDto { TaxStatus = TaxPostingStatus.NotTax };
    }
}
