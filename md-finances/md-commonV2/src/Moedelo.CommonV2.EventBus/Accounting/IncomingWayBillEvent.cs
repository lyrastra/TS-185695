namespace Moedelo.CommonV2.EventBus.Accounting
{
    public class IncomingWayBillEvent
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public long DocumentBaseId { get; set; }
        public int KontragentId { get; set; }

    }
}
