namespace Moedelo.CommonV2.EventBus.Bookkeeping
{
    public class MoneyBalanceCompletedEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }
    }
}