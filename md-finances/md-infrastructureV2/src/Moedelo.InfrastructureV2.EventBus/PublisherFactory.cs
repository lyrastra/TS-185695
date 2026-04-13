using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.EventBus.Internal.Pools;

namespace Moedelo.InfrastructureV2.EventBus
{
    [InjectAsSingleton]
    public class PublisherFactory : IPublisherFactory
    {
        private readonly IEventBusConfigurator eventBusConfigurator;
        private readonly ITopicExchangePublisherPool topicExchangePublisherPool;
        private readonly IAuditScopeManager auditScopeManager;

        public PublisherFactory(
            IEventBusConfigurator eventBusConfigurator,
            ITopicExchangePublisherPool topicExchangePublisherPool,
            IAuditScopeManager auditScopeManager)
        {
            this.eventBusConfigurator = eventBusConfigurator;
            this.topicExchangePublisherPool = topicExchangePublisherPool;
            this.auditScopeManager = auditScopeManager;
        }

        public IPublisher<T> GetForAllClient<T>(EventBusEventDefinition<T> definition) where T : class
        {
            return new AllClientPublisher<T>(definition, eventBusConfigurator, topicExchangePublisherPool, auditScopeManager);
        }
    }
}