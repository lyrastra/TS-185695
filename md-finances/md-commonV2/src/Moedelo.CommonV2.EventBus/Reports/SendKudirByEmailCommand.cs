namespace Moedelo.CommonV2.EventBus.Reports
{
    public class SendKudirByEmailCommand
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public int Year { get; set; }
    }
}
