using Moedelo.Payroll.Kafka.Abstractions.Events;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Payroll.Kafka.Abstractions
{
    public interface IWorkerCudEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IWorkerCudEventReaderBuilder OnCudEvent(Func<WorkerCudEventMessage, Task> onEvent);
        
        IWorkerCudEventReaderBuilder OnCudEvent(Func<WorkerCudEventMessage, KafkaMessageValueMetadata, Task> onEvent);

        IWorkerCudEventReaderBuilder OnFired(Func<WorkerFiredEventData, Task> onEvent);
    }
}
