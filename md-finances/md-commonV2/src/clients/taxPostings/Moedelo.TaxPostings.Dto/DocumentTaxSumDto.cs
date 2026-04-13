namespace Moedelo.TaxPostings.Dto
{
    /// <summary>
    /// Сумма налоговых проводок по BaseId
    /// </summary>
    public class DocumentTaxSumDto
    {
        /// <summary>
        /// BaseId документа, по которому создана проводка
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// Сумма проводки
        /// </summary>
        public decimal Sum { get; set; }
    }
}