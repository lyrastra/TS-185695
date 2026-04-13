namespace Moedelo.AccountingV2.Dto.Payments
{
    public class FundPaymentsDto
    {
        /// <summary>
        /// ФФОМС
        /// </summary>
        public decimal PaidFfoms { get; set; }

        /// <summary>
        /// ПФР
        /// </summary>
        public decimal PaidPfr { get; set; }

        /// <summary>
        /// ПФР до 2016 года
        /// </summary>
        public decimal PaidPfrBefore2016 { get; set; }

        /// <summary>
        /// ПФР с превышения 
        /// </summary>
        public decimal PaidPfrOverdraft { get; set; }

        /// <summary>
        /// ФСС нетрудоспособность
        /// </summary>
        public decimal PaidFssDisability { get; set; }

        /// <summary>
        /// ФСС травматизм
        /// </summary>
        public decimal PaidFssInjury { get; set; }

        public decimal NonPaidFfoms { get; set; }

        public decimal NonPaidPfr { get; set; }

        public decimal NonPaidPfrBefore2016 { get; set; }

        /// <summary>
        /// ПФР с превышения 
        /// </summary>
        public decimal NonPaidPfrOverdraft { get; set; }

        public decimal NonPaidFssDisability { get; set; }

        public decimal NonPaidFssInjury { get; set; }
    }
}
