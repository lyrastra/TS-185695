using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.EventBus.Internal.Pools;

namespace Moedelo.InfrastructureV2.EventBus
{
    internal sealed class DirectClientPublisher<T> : ClientPublisherBase<T>, IPublisher<T> where T : class
    {
        private readonly string connectionString;

        private readonly string exchangeName;

        private readonly string routingKey;

        public DirectClientPublisher(
            string connectionString, 
            string exchangeName, 
            string routingKey,
            ITopicExchangePublisherPool topicExchangePublisherPool,
            IAuditScopeManager auditScopeManager)
            : base(topicExchangePublisherPool, auditScopeManager)
        {
            this.connectionString = connectionString;
            this.exchangeName = exchangeName;
            this.routingKey = routingKey;
        }

        public Task PublishAsync(T data, uint currentRetryCount = 0, TimeSpan? delay = null)
        {
            return PublishAsync(connectionString, exchangeName, routingKey, data, currentRetryCount, delay);
        }
    }
}