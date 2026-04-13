using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Base.MessageActionWrappers
{
    public sealed class AuditMessageActionWrapper<T> : IMessageActionWrapper<T>
        where T : MoedeloKafkaMessageValueBase
    {
        private readonly string topicName;
        private readonly IAuditTracer auditTracer;

        public AuditMessageActionWrapper(string topicName, IAuditTracer auditTracer)
        {
            this.topicName = topicName;
            this.auditTracer = auditTracer;
        }

        public Func<T, Task> Wrap(Func<T, Task> onMessage)
        {
            return async message =>
            {
                var parentSpanContext = message.AuditSpanContext;

                using (var scope = auditTracer
                    .BuildSpan(AuditSpanType.EventApiHandler, $"Handle {typeof(T).Name} from {topicName}")
                    .AsChildOf(parentSpanContext).IgnoreTraceId()
                    .WithTag("Message", message)
                    .Start())
                {
                    try
                    {
                        await onMessage(message).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        scope.Span.SetError(ex);

                        throw;
                    }
                }
            };
        }
    }
}