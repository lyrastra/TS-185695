using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.EventBus.Internal.Pools;

namespace Moedelo.InfrastructureV2.EventBus
{
    internal sealed class AllClientPublisher<T> : ClientPublisherBase<T>, IPublisher<T> where T : class
    {
        private const string routingKey = "ClientId.All";

        private readonly EventBusEventDefinition<T> definition;

        private readonly IEventBusConfigurator eventBusConfigurator;

        public AllClientPublisher(
            EventBusEventDefinition<T> definition, 
            IEventBusConfigurator eventBusConfigurator,
            ITopicExchangePublisherPool topicExchangePublisherPool,
            IAuditScopeManager auditScopeManager) 
            : base(topicExchangePublisherPool, auditScopeManager)
        {
            this.definition = definition;
            this.eventBusConfigurator = eventBusConfigurator;
        }

        public Task PublishAsync(T data, uint currentRetryCount = 0, TimeSpan? delay = null)
        {
            var connectionString = eventBusConfigurator.GetEventBusConfig();
            var exchangeName = $"{eventBusConfigurator.GetNamePrefixConfig()}.MD.{definition.ExchangeName}.V2.E";
            
            return PublishAsync(connectionString, exchangeName, routingKey, data, currentRetryCount, delay);
        }
    }
}