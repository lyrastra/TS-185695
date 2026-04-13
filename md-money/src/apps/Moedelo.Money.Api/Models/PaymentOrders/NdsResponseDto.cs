using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders
{
    public class NdsResponseDto
    {
        /// <summary>
        /// В том числе НДС
        /// </summary>
        public bool IncludeNds { get; set; }

        /// <summary>
        /// Ставка НДС:
        /// -1 - Без НДС, 
        /// 0 - НДС 0%, 
        /// 1 - НДС 10%, 
        /// 2 - НДС 18%, 
        /// 3 - НДС 10/110, 
        /// 4 - НДС 18/118, 
        /// 5 - НДС 20%,
        /// 6 - НДС 20/120,
        /// 7 - НДС 5%,
        /// 8 - НДС 5/105,
        /// 9 - НДС 7%,
        /// 10 - НДС 7/107,
        /// 11 - НДС 22%,
        /// 12 - НДС 22/122.
        /// </summary>
        public NdsType? Type { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? Sum { get; set; }
    }
}
