using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.Bills.Commands;
using Moedelo.Billing.Kafka.Abstractions.Bills.Writers;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.Bills.Writers;

[InjectAsSingleton(typeof(ISendBillRemindersCommandWriter))]
public class SendBillRemindersCommandWriter(
    IMoedeloEntityCommandKafkaTopicWriter topicWriter,
    ILogger<ISendBillRemindersCommandWriter> logger)
    : ISendBillRemindersCommandWriter
{
    private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder =
        MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
            BillingTopics.Bills.Notification.CommandTopic,
            BillingTopics.Bills.Notification.EntityName);

    public Task WriteAsync(SendBillRemindersCommand message)
    {
        logger.LogInformationExtraData(message, $"Отправка команды {nameof(SendBillRemindersCommand)}");
        var commandDefinition = definitionBuilder.CreateCommandDefinition(message.RequestGuid.ToString(), message);
        
        return topicWriter.WriteCommandDataAsync(commandDefinition);
    }
}