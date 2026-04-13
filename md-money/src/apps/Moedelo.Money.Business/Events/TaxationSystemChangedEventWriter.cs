using System.Threading;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Events
{
    [InjectAsSingleton(typeof(ITaxationSystemChangedEventWriter))]
    internal sealed class TaxationSystemChangedEventWriter : MoedeloKafkaTopicWriterBase, ITaxationSystemChangedEventWriter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;

        public TaxationSystemChangedEventWriter(
            IExecutionInfoContextAccessor contextAccessor,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.contextAccessor = contextAccessor;
        }

        public Task WriteAsync(TaxationSystemChangedEvent changedEvent)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var key = context.FirmId.ToString();
            var value = new TaxationSystemChangedEventMessageValue
            {
                DocumentBaseId = changedEvent.DocumentBaseId,
                TaxationSystemType = changedEvent.TaxationSystemType,
                Guid = changedEvent.Guid
            };
            return WriteAsync(MoneyTopics.Events.TaxationSystemChangedEvent, key, value, CancellationToken.None);
        }
    }
}
