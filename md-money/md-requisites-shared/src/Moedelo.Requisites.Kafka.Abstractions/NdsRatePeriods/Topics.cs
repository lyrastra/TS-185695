namespace Moedelo.Requisites.Kafka.Abstractions.NdsRatePeriods
{
    public class Topics
    {
        private const string DomainName = "Requisites";
        
        //Entity
        public static class NdsRatePeriodsEntity
        {
            public const string EntityName = "NdsRatePeriods";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}