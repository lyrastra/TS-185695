using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions.Receipts.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.Abstractions.Receipts.Writers;

[InjectAsSingleton(typeof(INotRecognizedPaymentsCommandWriter))]
public class NotRecognizedPaymentsCommandWriter(
    ILogger<NotRecognizedPaymentsCommandWriter> logger,
    IMoedeloEntityCommandKafkaTopicWriter topicWriter)
    : INotRecognizedPaymentsCommandWriter
{
    private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder =
        MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
            BillingTopics.Receipts.Receipt.Command.Topic,
            BillingTopics.Receipts.Receipt.EntityName);

    
    public async Task WriteAsync(NotRecognizedPaymentsCommand command)
    {
        logger.LogInformationExtraData(command, $"Отправка команды {nameof(NotRecognizedPaymentsCommand)}");

        var commandDefinition = definitionBuilder.CreateCommandDefinition(Guid.NewGuid().ToString(), command);
        await topicWriter.WriteCommandDataAsync(commandDefinition);
    }
}