namespace Moedelo.BillingV2.Dto.Billing
{
    public class PayRequestDto
    {
        public PayRequestDto()
        {
            InDate = null;
            PaymentNumber = null;
            OutsourcingTariff = null;
            AgentId = 0;
            UserId = 0;
        }

        public int FirmId { get; set; }

        public int PriceListId { get; set; }

        public int SalerId { get; set; }

        public int PartnerId { get; set; }

        public string Method { get; set; }

        public string PromoCode { get; set; }

        public string Note { get; set; }

        public int PaymentId { get; set; }

        public int Summ { get; set; }

        /// <summary>
        /// Фиксированная стоимость, указанная при создании платежа,
        /// промокоды и региональные коэффициенты НЕ ПРИМЕНЯЮТСЯ 
        /// </summary>
        public int? FixedPrice { get; set; }

        public bool Success { get; set; }

        public bool ForceAny { get; set; }

        public string InDate { get; set; }

        public string PaymentNumber { get; set; }

        public int? OutsourcingTariff { get; set; }

        public int AgentId { get; set; }

        /// <summary>
        /// Здесь должен быть либо 0, либо mailUserId. Не допускать, чтобы здесь был id - пользователя партнёрки
        /// </summary>
        public int UserId { get; set; }

        public bool Tracked { get; set; }
    }
}