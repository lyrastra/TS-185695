using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.Bills.Commands;
using Moedelo.Billing.Kafka.Abstractions.Bills.Writers;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.Bills.Writers
{
    [InjectAsSingleton(typeof(IInvoiceBillCommandWriter))]
    public class InvoiceBillCommandWriter : IInvoiceBillCommandWriter
    {
        private readonly IMoedeloEntityCommandKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;
        private readonly ILogger logger;

        public InvoiceBillCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter topicWriter,
            ILogger<IInvoiceBillCommandWriter> logger)
        {
            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
                BillingTopics.Bills.Bill.CommandTopic,
                BillingTopics.Bills.Bill.EntityName);
            this.topicWriter = topicWriter;
            this.logger = logger;
        }

        public Task WriteAsync(InvoiceBillKafkaCommandData message)
        {
            logger.LogInformationExtraData(message, $"Отправка команды {nameof(InvoiceBillKafkaCommandData)}");

            return topicWriter.WriteCommandDataAsync(
                definitionBuilder.CreateCommandDefinition(message.RequestGuid.ToString(), message));
        }
    }
}