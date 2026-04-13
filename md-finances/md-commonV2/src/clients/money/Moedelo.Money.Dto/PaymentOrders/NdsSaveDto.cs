namespace Moedelo.Money.Dto.PaymentOrders
{
    public class NdsSaveDto
    {
        /// <summary>
        /// В том числе НДС
        /// </summary>
        public bool IncludeNds { get; set; }

        /// <summary>
        /// Ставка НДС
        /// </summary>
        public NdsType? Type { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal Sum { get; set; }
    }
}
