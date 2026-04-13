namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class MediationDto
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