namespace Moedelo.Money.Kafka.Abstractions.Models
{
    public class RentPeriod
    {
        /// <summary>
        /// Идентификатор строки графика платежей из договора аренды
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сумма платежа за период
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Описание периода
        /// </summary>
        public string Description { get; set; }
    }
}
