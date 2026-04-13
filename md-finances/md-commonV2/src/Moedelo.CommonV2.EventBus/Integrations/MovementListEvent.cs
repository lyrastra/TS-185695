namespace Moedelo.CommonV2.EventBus.Integrations
{
    public class MovementListEvent
    {
        public int FirmId { get; set; }
        public bool IsManual { get; set; }
        public string MongoObjectId { get; set; }
    }
}