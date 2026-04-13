using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class NdsDto
    {
        public bool IncludeNds { get; set; }

        /// <summary>
        /// Ставка НДС
        /// </summary>
        public NdsType? Type { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? Sum { get; set; }
    }
}
