using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.RetryData
{
    public class RetryDataWrapper<T> : IEntityCommandData
    {
        // repeat data
        public int RetryCount { get; set; }

        public int RepeateEventId { get; set; }


        public T Data { get; set; }
    }
}
