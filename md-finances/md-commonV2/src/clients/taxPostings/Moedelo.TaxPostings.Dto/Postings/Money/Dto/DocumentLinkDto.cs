namespace Moedelo.TaxPostings.Dto.Postings.Money.Dto
{
    /// <summary>
    /// Связь с первичным документом
    /// </summary>
    public class DocumentLinkDto
    {
        /// <summary>
        /// Идентификатор первичного документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Учитываемая сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public int Type { get; set; }
    }
}
