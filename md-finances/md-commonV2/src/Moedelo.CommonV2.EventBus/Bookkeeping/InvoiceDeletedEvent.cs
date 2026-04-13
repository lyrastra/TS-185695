namespace Moedelo.CommonV2.EventBus.Bookkeeping
{
    public class InvoiceDeletedEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public long DocumentBaseId { get; set; }
    }
}