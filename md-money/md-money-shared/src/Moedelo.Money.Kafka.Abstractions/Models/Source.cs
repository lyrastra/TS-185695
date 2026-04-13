using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.Models
{
    public class Source
    {
        public long Id { get; set; }

        /// <summary>
        /// Источник: банк/касса/платежные системы
        /// </summary>
        public OperationSource Type { get; set; }
    }
}
