namespace Moedelo.Docs.Dto.PurchaseUpd.Rest
{
    public class PurchaseUpdPaymentsSaveRequestDto
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