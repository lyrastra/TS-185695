namespace Moedelo.Docs.Dto.SalesUpd.Rest
{
    public class SalesUpdPaymentSaveRequestRestDto
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