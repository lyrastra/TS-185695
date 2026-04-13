using Moedelo.Stock.Enums;

namespace Moedelo.Stock.Kafka.Abstractions.Operations.Models
{
    public class StockOperationOverProductModel
    {
        public long ProductId { get; set; }

        public long StockId { get; set; }

        public StockOperationTypeEnum Type { get; set; }

        /// <summary>
        /// Так иногда бывает, например, при инвентаризации.
        /// Скорее всего это проблема в домене, но приходится мириться.
        /// Как реагировать на null в этом поле остается на усмотрение того,
        /// кто будет пользоваться данными событиями.
        /// </summary>
        public bool? IsPositive { get; set; }

        public decimal Count { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { set; get; }
    }
}
