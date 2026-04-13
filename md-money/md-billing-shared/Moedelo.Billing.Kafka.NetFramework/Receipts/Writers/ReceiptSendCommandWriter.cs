using System;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Common;
using Moedelo.Billing.Kafka.NetFramework.Abstractions.Receipts.Commands;
using Moedelo.Billing.Kafka.NetFramework.Abstractions.Receipts.Writers;
using Moedelo.ExecutionContext.Client;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.Billing.Kafka.Abstractions.Receipts.Writers;

[InjectAsSingleton(typeof(IReceiptSendCommandWriter))]
public class ReceiptSendCommandWriter(
    ILogger logger,
    ITokenApiClient tokenApiClient,
    IMoedeloEntityCommandKafkaTopicWriter topicWriter)
    : IReceiptSendCommandWriter
{
    private const string Tag = nameof(ReceiptSendCommandWriter); 

    public async Task WriteAsync(ReceiptSendCommand command)
    {
        logger.Info(Tag, $"Отправка команды {nameof(ReceiptSendCommand)}", extraData: new { command });

        var token = await tokenApiClient.GetUnidentified().ConfigureAwait(false);
        var commandKey = command?.FirmId?.ToString()
                         ?? command?.PaymentHistoryId?.ToString()
                         ?? (command?.PaymentImportDetailIds?.Any() == true
                             ? command.PaymentImportDetailIds.FirstOrDefault().ToString()
                             : null)
                         ?? command?.YooKassaPaymentGuids?.FirstOrDefault()
                         ?? command?.YaPayOrderGuids?.FirstOrDefault()
                         ?? Guid.NewGuid().ToString(); 

        await topicWriter.WriteCommandDataAsync(
            BillingTopics.Receipts.Receipt.Command.Topic,
            commandKey,
            BillingTopics.Receipts.Receipt.EntityName,
            command,
            token
        ).ConfigureAwait(false);
    }
}