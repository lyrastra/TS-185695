namespace Moedelo.Docs.Dto.PurchaseUpd.Rest
{
    public class PurchaseUpdPaymentDto
    {
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Сумма связи
        /// </summary>
        public decimal Sum { get; set; }
    }
}