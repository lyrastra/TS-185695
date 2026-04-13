namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Import
{
    public class SkippedImportReason
    {
        public bool IsInClosedPeriod { get; set; }

        public int? ImportLogId { get; set; }
    }
}
