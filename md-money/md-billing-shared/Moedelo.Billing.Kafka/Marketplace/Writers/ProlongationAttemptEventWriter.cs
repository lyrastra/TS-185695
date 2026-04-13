using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.Marketplace.Events;
using Moedelo.Billing.Kafka.Abstractions.Marketplace.Writers;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.Marketplace.Writers;

[InjectAsSingleton(typeof(IProlongationAttemptEventWriter))]
public class ProlongationAttemptEventWriter(
    IMoedeloEntityEventKafkaTopicWriter topicWriter) : IProlongationAttemptEventWriter
{
    private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder messageDefinitionBuilder =
        MoedeloEntityEventKafkaMessageDefinitionBuilder.For(
            BillingTopics.Marketplace.ProlongationAttempt.Event.Topic,
            BillingTopics.Marketplace.ProlongationAttempt.EntityName);

    public Task WriteAsync(ProlongationAttemptEvent eventData)
    {
        var key = eventData.FirmId.ToString();
        var definition = messageDefinitionBuilder.CreateEventDefinition(key, eventData);

        return topicWriter.WriteEventDataAsync(definition);
    }
}