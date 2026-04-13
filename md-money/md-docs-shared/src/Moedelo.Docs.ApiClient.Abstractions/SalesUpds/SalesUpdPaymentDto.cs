namespace Moedelo.Docs.ApiClient.Abstractions.SalesUpds
{
    public class SalesUpdPaymentDto
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