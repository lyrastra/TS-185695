using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Commands;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Docs.Kafka.Sales.Ukds
{
    [InjectAsSingleton(typeof(IUkdRefundPaymentCommandWriter))]
    public class UkdRefundPaymentCommandWriter : IUkdRefundPaymentCommandWriter
    {
        private const string Topic = AccountingPrimaryDocumentsTopics.Sales.Ukds.UpdateRefundPaymentCommand;
        private const string EntityName = nameof(AccountingPrimaryDocumentsTopics.Sales.Ukds.UpdateRefundPaymentCommand);

        private readonly IMoedeloEntityCommandKafkaTopicWriter moedeloEntityCommandKafkaTopicWriter;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public UkdRefundPaymentCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter moedeloEntityCommandKafkaTopicWriter)
        {
            this.moedeloEntityCommandKafkaTopicWriter = moedeloEntityCommandKafkaTopicWriter;
            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteAsync(long documentBaseId)
        {

            var commandData = new UkdUpdateRefundPayment
            {
                DocumentBaseId = documentBaseId
            };

            var commandDefinition = definitionBuilder.CreateCommandDefinition(commandData.DocumentBaseId.ToString(), commandData);
            return moedeloEntityCommandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}