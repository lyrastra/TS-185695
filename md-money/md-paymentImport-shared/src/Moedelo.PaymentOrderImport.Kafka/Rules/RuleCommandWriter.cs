using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.PaymentOrderImport.Kafka.Abstractions;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Rules;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Rules.Commands;
using System.Threading.Tasks;

namespace Moedelo.PaymentOrderImport.Kafka.Rules
{
    [InjectAsSingleton(typeof(IRuleCommandWriter))]
    class RuleCommandWriter : IRuleCommandWriter
    {
        private readonly string TopicName = ImportTopics.Rule.Commands.Topic;
        private readonly string EntityName = ImportTopics.Rule.EntityName;

        private readonly IMoedeloEntityCommandKafkaTopicWriter moedeloEntityCommandKafkaTopicWriter;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public RuleCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter moedeloEntityCommandKafkaTopicWriter)
        {
            this.moedeloEntityCommandKafkaTopicWriter = moedeloEntityCommandKafkaTopicWriter;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(TopicName, EntityName);
        }

        public Task WriteRuleCommandApplyIgnoreNumberAsync(ApplyIgnoreNumberCommand commandData)
        {
            var commandDefinition = definitionBuilder.CreateCommandDefinition(commandData.RuleId.ToString(), commandData);
            return moedeloEntityCommandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}
