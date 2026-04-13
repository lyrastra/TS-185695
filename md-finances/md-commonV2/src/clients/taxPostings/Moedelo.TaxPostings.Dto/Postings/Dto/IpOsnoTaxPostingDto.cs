using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.TaxPostings.Dto.Postings.Dto
{
    public class IpOsnoTaxPostingDto : ITaxPostingDto
    {
        /// <summary>
        /// Сумма проводки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Направление движения денег (приход/расход)
        /// </summary>
        public TaxPostingsDirection Direction { get; set; }
    }
}
