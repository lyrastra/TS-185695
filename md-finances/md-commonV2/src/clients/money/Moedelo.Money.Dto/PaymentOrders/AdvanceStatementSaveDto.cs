namespace Moedelo.Money.Dto.PaymentOrders
{
    /// <summary>
    /// Связь с авансовым документом
    /// </summary>
    public class AdvanceStatementLinkSaveDto
    {
        /// <summary>
        /// Идентификатор первичного документа
        /// </summary>
        public long DocumentBaseId { get; set; }
    }
}
