namespace Moedelo.CommonV2.EventBus.Billing
{
    public class AffiliatedFirmAddedEvent
    {
        /// <summary>
        /// основная фирма
        /// </summary>
        public int MainFirmId { get; set; }
        /// <summary>
        /// созданная фирма
        /// </summary>
        public int AffiliatedFirmId { get; set; }
        /// <summary>
        /// кто создал
        /// </summary>
        public int UserId { get; set; }
    }
}