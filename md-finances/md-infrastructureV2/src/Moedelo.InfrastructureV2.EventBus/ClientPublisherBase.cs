using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.EventBus.Internal;
using Moedelo.InfrastructureV2.EventBus.Internal.Pools;

namespace Moedelo.InfrastructureV2.EventBus
{
    internal abstract class ClientPublisherBase<T> where T : class
    {
        private readonly ITopicExchangePublisherPool topicExchangePublisherPool;
        private readonly IAuditScopeManager auditScopeManager;

        protected ClientPublisherBase(
            ITopicExchangePublisherPool topicExchangePublisherPool,
            IAuditScopeManager auditScopeManager)
        {
            this.topicExchangePublisherPool = topicExchangePublisherPool;
            this.auditScopeManager = auditScopeManager;
        }

        protected async Task PublishAsync(
            string connectionString, 
            string exchangeName, 
            string routingKey, 
            T data,
            uint currentRetryCount, 
            TimeSpan? delay)
        {
            var topicExchangePublisher = topicExchangePublisherPool.GetTopicExchangePublisher(
                new ExchangeConnection(connectionString, exchangeName));
            var maxDelay = TimeSpan.FromDays(1);
            var messageDelay = delay.HasValue == false
                ? (TimeSpan?) null
                : maxDelay < delay.Value
                    ? maxDelay
                    : delay.Value;
            var dataWrapper = new MessageWrapper<T>
            {
                Data = data, 
                RepeatCount = currentRetryCount, 
                Delay = messageDelay
            };
            
            var auditSpanContext = auditScopeManager.Current?.Span?.Context;
            
            if (auditSpanContext != null)
            {
                dataWrapper.AuditSpanContext = new AuditSpanContext
                {
                    AsyncTraceId = auditSpanContext.AsyncTraceId,
                    TraceId = auditSpanContext.TraceId,
                    ParentId = auditSpanContext.ParentId,
                    CurrentId = auditSpanContext.CurrentId,
                    CurrentDepth = auditSpanContext.CurrentDepth,
                };
            }
            
            var delayMs = messageDelay.HasValue == false
                ? (int?) null
                : (int) messageDelay.Value.TotalMilliseconds;
            await topicExchangePublisher.PublishAsync(routingKey, dataWrapper, delayMs).ConfigureAwait(false);
        }
    }
}