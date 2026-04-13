using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.BillManagement;
using Moedelo.Billing.Kafka.Abstractions.BillManagement.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.BillManagement;

[InjectAsSingleton(typeof(IBillManagementBillChangingEventWriter))]
public class BillManagementBillChangingEventWriter(
    IMoedeloEntityEventKafkaTopicWriter topicWriter) : IBillManagementBillChangingEventWriter
{
    private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter = topicWriter;
    private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder messageDefinitionBuilder =
        MoedeloEntityEventKafkaMessageDefinitionBuilder.For(
            BillingTopics.BillManagement.BillChanging.Event.Topic,
            BillingTopics.BillManagement.BillChanging.EntityName);

    public Task WriteAsync(BillChangingStateChangedEvent eventData)
    {
        var key = eventData.FirmId.ToString();
        var definition = messageDefinitionBuilder.CreateEventDefinition(key, eventData);

        return topicWriter.WriteEventDataAsync(definition);
    }
}
