namespace Moedelo.BizV2.Dto.Kudir
{
    /// <summary>
    /// Сумма взносов в фонд за себя (за ИП)
    /// </summary>
    public class FundPaymentSumForUsnIpDto
    {
        /// <summary>
        /// Сумма ФВ (фиксированного взноса)
        /// </summary>
        public decimal FixedPaymentSum { get; set; }

        /// <summary>
        /// Сумма ДВ (дополнительного взноса)
        /// </summary>
        public decimal AdditionalPaymentSum { get; set; }

        /// <summary>
        /// Сумма ДВ (дополнительного взноса) за предыдущий год
        /// </summary>
        public decimal AdditionalPaymentPreviousYearSum { get; set; }
    }
}
