using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Common;
using Moedelo.Billing.Kafka.NetFramework.Abstractions.Marketplace.Events;
using Moedelo.Billing.Kafka.NetFramework.Abstractions.Marketplace.Writers;
using Moedelo.ExecutionContext.Client;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.Billing.Kafka.NetFramework.Marketplace.Writers;

[InjectAsSingleton(typeof(IProlongationAttemptEventWriter))]
public class ProlongationAttemptEventWriter(
    ILogger logger,
    ITokenApiClient tokenApiClient,
    IMoedeloEntityEventKafkaTopicWriter topicWriter) : IProlongationAttemptEventWriter
{
    private const string Tag = nameof(ProlongationAttemptEventWriter); 

    public async Task WriteAsync(ProlongationAttemptEvent eventData)
    {
        logger.Info(Tag, $"Отправка события {nameof(ProlongationAttemptEvent)}", extraData: new { eventData });

        var token = await tokenApiClient.GetUnidentified().ConfigureAwait(false);
        var commandKey = eventData.FirmId.ToString();

        await topicWriter.WriteEventDataAsync(
            BillingTopics.Marketplace.ProlongationAttempt.Event.Topic,
            commandKey,
            BillingTopics.Marketplace.ProlongationAttempt.EntityName,
            eventData,
            token
        ).ConfigureAwait(false);
        
    }
}