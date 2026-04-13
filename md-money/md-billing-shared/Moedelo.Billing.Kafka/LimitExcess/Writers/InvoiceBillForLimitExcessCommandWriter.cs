using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Kafka.Abstractions;
using Moedelo.Billing.Kafka.Abstractions.LimitExcess.Writers;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Billing.Kafka.LimitExcess.Writers
{
    [InjectAsSingleton(typeof(IInvoiceBillForLimitExcessCommandWriter))]
    public class InvoiceBillForLimitExcessCommandWriter : IInvoiceBillForLimitExcessCommandWriter
    {
        private readonly IMoedeloEntityCommandKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;
        private readonly ILogger logger;

        public InvoiceBillForLimitExcessCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter topicWriter,
            ILogger<IInvoiceBillForLimitExcessCommandWriter> logger)
        {
            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
                BillingTopics.LimitExcess.FirmLimitExcess.CommandTopic,
                BillingTopics.LimitExcess.FirmLimitExcess.EntityName);
            this.topicWriter = topicWriter;
            this.logger = logger;
        }

        public Task WriteAsync<T>(T message, string key) where T : IEntityCommandData
        {
            logger.LogInformationExtraData(message, $"Отправка события {typeof(T).Name}");

            return topicWriter.WriteCommandDataAsync(definitionBuilder.CreateCommandDefinition(key, message));
        }
    }
}