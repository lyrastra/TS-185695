using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions.Events;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Payroll.Kafka
{
    [InjectAsSingleton]
    public class WorkerCudEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IWorkerCudEventReaderBuilder
    {
        private const string EntityType = "Worker";
        
        public WorkerCudEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                PayrollTopics.Events.WorkerCudEvent,
                EntityType,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IWorkerCudEventReaderBuilder OnCudEvent(Func<WorkerCudEventMessage, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
        
        public IWorkerCudEventReaderBuilder OnCudEvent(Func<WorkerCudEventMessage, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
        
        public IWorkerCudEventReaderBuilder OnFired(Func<WorkerFiredEventData, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}
