namespace Moedelo.AccountingV2.Dto.Payments
{
    public class WorkerPaymentDto
    {
        public int WorkerId { get; set; }

        /// <summary>
        /// зарплата
        /// </summary>
        public decimal PaymentByWorker { get; set; }

        /// <summary>
        /// дивиденды
        /// </summary>
        public decimal PaymentByDividends { get; set; }

        /// <summary>
        /// ГПД
        /// </summary>
        public decimal PaymentByContract { get; set; }

        /// <summary>
        /// выдача под отчет
        /// </summary>
        public decimal PaymentByAccountablePerson { get; set; }

        public decimal NonPaidByWorker { get; set; }

        public decimal NonPaidByContract { get; set; }

        public decimal NonPaidByAccountablePerson { get; set; }

        public decimal NonPaidByDividends { get; set; }
    }
}