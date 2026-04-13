namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Dto
{
    public class PaymentDto
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