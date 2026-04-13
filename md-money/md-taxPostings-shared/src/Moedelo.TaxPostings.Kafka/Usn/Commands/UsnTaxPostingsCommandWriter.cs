using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.TaxPostings.Kafka.Abstractions;
using Moedelo.TaxPostings.Kafka.Abstractions.Usn.Commands;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Kafka.Usn.Commands
{
    [InjectAsSingleton(typeof(IUsnTaxPostingsCommandWriter))]
    internal sealed class UsnTaxPostingsCommandWriter : IUsnTaxPostingsCommandWriter
    {
        private static readonly string TopicName = TaxPostingsTopics.Usn.Command.Topic;
        private static readonly string EntityName = TaxPostingsTopics.Usn.EntityName;

        private readonly IMoedeloEntityCommandKafkaTopicWriter moedeloEntityCommandKafkaTopicWriter;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public UsnTaxPostingsCommandWriter(IMoedeloEntityCommandKafkaTopicWriter moedeloEntityCommandKafkaTopicWriter)
        {
            this.moedeloEntityCommandKafkaTopicWriter = moedeloEntityCommandKafkaTopicWriter;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(TopicName, EntityName);
        }

        public Task WriteAppendAsync(AppendUsnTaxPostings commandData)
        {
            var commandDefinition = definitionBuilder.CreateCommandDefinition(commandData.DocumentBaseId.ToString(), commandData);
            return moedeloEntityCommandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteOverwriteAsync(OverwriteUsnTaxPostings commandData)
        {
            var commandDefinition = definitionBuilder.CreateCommandDefinition(commandData.DocumentBaseId.ToString(), commandData);
            return moedeloEntityCommandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteDeleteAsync(DeleteUsnTaxPostings commandData)
        {
            var commandDefinition = definitionBuilder.CreateCommandDefinition(commandData.DocumentBaseId.ToString(), commandData);
            return moedeloEntityCommandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}
