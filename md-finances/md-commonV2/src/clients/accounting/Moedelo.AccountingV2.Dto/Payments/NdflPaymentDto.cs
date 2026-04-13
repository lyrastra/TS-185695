namespace Moedelo.AccountingV2.Dto.Payments
{
    public class NdflPaymentDto
    {
        public NdflPaymentDto()
        {
            this.PaidNdfl = 0;
            this.IncludeNonPaidNdfl = 0;
        }

        /// <summary>
        /// НДФЛ сумма только по оплаченным документам
        /// </summary>
        public decimal PaidNdfl { get; set; }

        /// <summary>
        /// НДФЛ сумма по всем документам, включая не оплаченные
        /// </summary>
        public decimal IncludeNonPaidNdfl { get; set; }
    }
}