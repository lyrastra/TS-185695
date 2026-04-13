namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer
{
    public class MediationResponseDto
    {
        /// <summary>
        /// Признак: посредничество
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Комиссия посредника
        /// </summary>
        public decimal? CommissionSum { get; set; }
    }
}