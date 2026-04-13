namespace Moedelo.CommonV2.EventBus.FirmActivityOffers
{
    public class FirmActivityOffersTriggerFiredEvent
    {
        public int FirmId { get; set; }
        
        public string Code { get; set; }
        
        public string Activity { get; set; }
        
        public string Context { get; set; }
    }
}