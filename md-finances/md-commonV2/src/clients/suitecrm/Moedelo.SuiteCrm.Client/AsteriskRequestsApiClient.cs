using System;
using System.Threading.Tasks;
using Moedelo.CommonV2.EventBus;
using Moedelo.CommonV2.EventBus.Crm;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class AsteriskRequestsApiClient : IAsteriskRequestsApiClient
    {
        private readonly IPublisher<SuiteCrmCrudTaskEvent> publisherTaskSendEvent;
        private readonly IPublisher<SuiteCrmCrudTaskEvent> publisherTaskDeleteEvent;

        public AsteriskRequestsApiClient(IPublisherFactory publisherFactory)
        {
            publisherTaskSendEvent = publisherFactory.GetForAllClient(EventBusMessages.SuiteCrmBlTaskSendToAsterisk);
            publisherTaskDeleteEvent = publisherFactory.GetForAllClient(EventBusMessages.SuiteCrmBlTaskDeleteFromAsterisk);
        }

        public Task SendTaskToAsteriskAsync(string taskId)
        {
            return publisherTaskSendEvent.PublishAsync(new SuiteCrmCrudTaskEvent {TaskId = taskId, Timestamp = DateTime.UtcNow});
        }

        public Task DeleteTaskFromAsteriskAsync(string taskId)
        {
            return publisherTaskDeleteEvent.PublishAsync(new SuiteCrmCrudTaskEvent {TaskId = taskId, Timestamp = DateTime.UtcNow});
        }
    }
}