using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(RetailRevenueEventWriter))]
    internal class RetailRevenueEventWriter : MoedeloKafkaTopicWriterBase
    {
        private readonly string Topic = MoneyTopics.CashOrders.RetailRevenue.Event.Topic;
        private readonly string EntityName = MoneyTopics.CashOrders.RetailRevenue.EntityName;
        
        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public RetailRevenueEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteUpdatedEventAsync(RetailRevenueSaveRequest request)
        {
            var eventData = RetailRevenueMapper.MapToUpdatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}