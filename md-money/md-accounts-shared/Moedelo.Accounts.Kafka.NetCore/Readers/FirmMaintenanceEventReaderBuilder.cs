using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Kafka.Abstractions;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Maintenance;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Accounts.Kafka.NetCore.Readers
{
    [InjectAsSingleton(typeof(IFirmMaintenanceEventReaderBuilder))]
    public sealed class FirmMaintenanceEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IFirmMaintenanceEventReaderBuilder
    {
        public FirmMaintenanceEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer,
            ILogger<FirmMaintenanceEventReaderBuilder> logger)
            : base(
                Topics.FirmMaintenance.Event.Topic,
                Topics.FirmMaintenance.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer,
                logger)
        {
        }
        
        public IFirmMaintenanceEventReaderBuilder OnFirmsWereDeletedEvent(
            Func<FirmsWereDeletedEvent, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }
    }
}