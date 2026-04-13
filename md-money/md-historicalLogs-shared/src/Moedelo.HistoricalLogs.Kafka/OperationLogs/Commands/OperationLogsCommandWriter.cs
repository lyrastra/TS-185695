using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs;
using Moedelo.HistoricalLogs.Kafka.Abstractions.OperationLogs.Commands;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.HistoricalLogs.Kafka.OperationLogs.Commands;

[InjectAsSingleton(typeof(IOperationLogsCommandWriter))]
internal sealed class OperationLogsCommandWriter : IOperationLogsCommandWriter
{
    private static readonly string TopicName = Topics.OperationLogs.Command.Topic;
    private const string EntityName = Topics.OperationLogs.EntityName;

    private readonly IMoedeloEntityCommandKafkaTopicWriter moedeloEntityCommandKafkaTopicWriter;
    private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

    public OperationLogsCommandWriter(IMoedeloEntityCommandKafkaTopicWriter moedeloEntityCommandKafkaTopicWriter)
    {
        this.moedeloEntityCommandKafkaTopicWriter = moedeloEntityCommandKafkaTopicWriter;

        definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(TopicName, EntityName);
    }

    public Task WriteLogAsync(WriteOperationLog commandData)
    {
        var commandDefinition = definitionBuilder.CreateCommandDefinition(commandData.ObjectId.ToString(), commandData);
        return moedeloEntityCommandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
    }

    public Task WriteLogAsync(IdentifiedWriteOperationLog commandData)
    {
        var commandDefinition = definitionBuilder.CreateCommandDefinition(commandData.ObjectId.ToString(), commandData);

        return moedeloEntityCommandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
    }

    public async Task WriteLogAsync(IReadOnlyCollection<IdentifiedWriteOperationLog> commandDataList)
    {
        var commandDefinitionList = commandDataList
            .Select(commandData =>
                definitionBuilder.CreateCommandDefinition(commandData.ObjectId.ToString(), commandData));

        await moedeloEntityCommandKafkaTopicWriter
            .QueueToWriteCommandDataListAsync(commandDefinitionList, CancellationToken.None);
    }
}