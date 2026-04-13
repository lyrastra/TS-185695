using Moedelo.Common.Enums.Enums.YandexKassaPayments;

namespace Moedelo.BillingV2.Dto.YandexKassaPayments
{
    public class YandexKassaPaymentDto
    {
        public int FirmId { get; set; }
        public string YandexKassaPaymentGuid { get; set; }
        public int? PaymentHistoryId { get; set; }
        public bool IsGlavuchet { get; set; }
        public YandexKassaPaymentStatus Status { get; set; }
        public string PaymentData { get; set; }
    }
}