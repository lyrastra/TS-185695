using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.RabbitMQ.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Interfaces;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Models;

namespace Moedelo.Common.RabbitMQ
{
    [InjectAsSingleton(typeof(IMoedeloRabbitMqWriter))]
    internal class MoedeloRabbitMqWriter : IMoedeloRabbitMqWriter
    {
        private readonly IMoedeloRabbitMqConfigurator moedeloRabbitMqConfigurator;
        private readonly IAuditScopeManager auditScopeManager;
        private readonly IRabbitMqProducer rabbitMqProducer;

        public MoedeloRabbitMqWriter(IMoedeloRabbitMqConfigurator moedeloRabbitMqConfigurator, IAuditScopeManager auditScopeManager, IRabbitMqProducer rabbitMqProducer)
        {
            this.moedeloRabbitMqConfigurator = moedeloRabbitMqConfigurator;
            this.auditScopeManager = auditScopeManager;
            this.rabbitMqProducer = rabbitMqProducer;
        }

        public async Task WriteAsync<T>(string exchangeName, T data, uint repeatCount = 0, TimeSpan? delay = null)
        {
            if (string.IsNullOrEmpty(exchangeName))
            {
                throw new ArgumentNullException(nameof(exchangeName));
            }

            var connectionString = moedeloRabbitMqConfigurator.GetConnection();
            var fullExchangeName = $"{moedeloRabbitMqConfigurator.GetExchangeNamePrefix()}.MD.{exchangeName}.V2.E";

            var dataWrapper = new MessageWrapper<T>
            {
                Data = data,
                RepeatCount = repeatCount,
                Delay = delay
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

            var delayMs = delay.HasValue == false
                ? (int?) null
                : (int) delay.Value.TotalMilliseconds;

            await rabbitMqProducer.ProduceAsync(
                new ProducerExchangeConnection(connectionString, fullExchangeName),
                "ClientId.All", dataWrapper, delayMs);
        }
    }
}
