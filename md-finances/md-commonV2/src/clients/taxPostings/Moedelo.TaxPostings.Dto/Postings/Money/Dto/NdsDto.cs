namespace Moedelo.TaxPostings.Dto.Postings.Money.Dto
{
    public class NdsDto
    {
        /// <summary>
        /// В том числе НДС
        /// </summary>
        public bool IncludeNds { get; set; }

        /// <summary>
        /// Ставка НДС
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? Sum { get; set; }
    }
}
