namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.AccPostings
{
    public class SubcontoDto
    {
        /// <summary>
        /// Идентификатор субконто
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public int Type { get; set; }
    }
}
