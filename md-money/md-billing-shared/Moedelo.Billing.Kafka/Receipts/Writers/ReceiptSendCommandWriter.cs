using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions.Receipts.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.Abstractions.Receipts.Writers;

[InjectAsSingleton(typeof(IReceiptSendCommandWriter))]
public class ReceiptSendCommandWriter(
    ILogger<ReceiptSendCommandWriter> logger,
    IMoedeloEntityCommandKafkaTopicWriter topicWriter)
    : IReceiptSendCommandWriter
{
    private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder =
        MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
            BillingTopics.Receipts.Receipt.Command.Topic,
            BillingTopics.Receipts.Receipt.EntityName);
    
    public async Task WriteAsync(ReceiptSendCommand command)
    {
        logger.LogInformationExtraData(command, $"Отправка команды {nameof(ReceiptSendCommand)}");
        
        var commandKey = command?.FirmId?.ToString()
                         ?? command?.PaymentHistoryId?.ToString()
                         ?? (command?.PaymentImportDetailIds?.Any() == true
                             ? command.PaymentImportDetailIds.FirstOrDefault().ToString()
                             : null)
                         ?? command?.YooKassaPaymentGuids?.FirstOrDefault()
                         ?? command?.YaPayOrderGuids?.FirstOrDefault()
                         ?? Guid.NewGuid().ToString(); 
        var commandDefinition = definitionBuilder.CreateCommandDefinition(commandKey, command);
        await topicWriter.WriteCommandDataAsync(commandDefinition);
    }
}