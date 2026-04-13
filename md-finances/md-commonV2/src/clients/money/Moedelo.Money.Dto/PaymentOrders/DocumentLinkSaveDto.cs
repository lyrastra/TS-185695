namespace Moedelo.Money.Dto.PaymentOrders
{
    /// <summary>
    /// Связь с первичным документом
    /// </summary>
    public class DocumentLinkSaveDto
    {
        /// <summary>
        /// Идентификатор первичного документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Учитываемая сумма
        /// </summary>
        public decimal Sum { get; set; }
    }
}
