namespace Moedelo.BillingV2.Dto.BackofficeBilling.Bills
{
    public class BackofficeBillingBillResponseDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор основного счета
        /// </summary>
        public int PrimaryBillId { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }
        public int? PaymentHistoryId { get; set; }
    }
}
