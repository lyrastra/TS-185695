using Moedelo.Common.Enums.Enums.Partner;

namespace Moedelo.CommonV2.EventBus.Backoffice.PaymentImport
{
    public class LinkMappedPaymentCommand
    {
        public enum RecognizeTypeEnum : byte
        {
            None = 0,
            Automatically = 1,
            Manually = 2,
            ManuallyByParts = 3,
            ManuallyById = 4
        }

        public int UserId { get; set; }
        public int TargetPaymentImportDetailId { get; set; }
        public int PaymentHistoryId { get; set; }
        public RecognizeTypeEnum RecognizeType { get; set; }
        public int[] AllPaymentPartIds { get; set; }

        /// <summary>
        /// Тип регионального партнёра
        /// </summary>
        public RegionalPartnerType? RegionalPartnerType { get; set; }
    }
}
