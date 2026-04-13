using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.AutoBilling.Commands;
using Moedelo.Billing.Kafka.Abstractions.AutoBilling.Writers;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.AutoBilling.Writers;

[InjectAsSingleton(typeof(IInitiateCommandWriter))]
internal class InitiateCommandWriter : IInitiateCommandWriter
{
    private readonly IMoedeloEntityCommandKafkaTopicWriter topicWriter;
    private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;
    private readonly ILogger<InitiateCommandWriter> logger;

    public InitiateCommandWriter(
        IMoedeloEntityCommandKafkaTopicWriter topicWriter,
        ILogger<InitiateCommandWriter> logger)
    {
        definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
            BillingTopics.Initiate.Command.Topic,
            BillingTopics.Initiate.EntityName);
        this.topicWriter = topicWriter;
        this.logger = logger;
    }

    public Task SendStartAutoInitiateCommandAsync(StartAutoInitiateCommand commandData)
    {
        return WriteAsync(commandData, Guid.NewGuid().ToString());
    }

    private Task WriteAsync<T>(T message, string key) where T : IEntityCommandData
    {
        logger.LogInformationExtraData(message, $"Отправка команды {typeof(T).Name}");

        return topicWriter.WriteCommandDataAsync(definitionBuilder.CreateCommandDefinition(key, message));
    }
}