namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto
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