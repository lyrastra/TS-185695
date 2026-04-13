using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson.Events;
using Moedelo.Money.Registry.Api.Mappers.CashOrders.Incoming;
using Moedelo.Money.Registry.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;

namespace Moedelo.Money.Registry.Api.HostedServices.CashOrders.Incoming
{
    public class RefundFromAccountablePersonHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IRefundFromAccountablePersonEventReaderBuilder eventReaderBuilder;
        private readonly IRegistryOperationUpdater registryOperationUpdater;
        private readonly SettingValue consumerCountSetting;

        public RefundFromAccountablePersonHostedService(
            ILogger<RefundFromAccountablePersonHostedService> logger,
            ISettingRepository settingRepository,
            IRefundFromAccountablePersonEventReaderBuilder eventReaderBuilder,
            IRegistryOperationUpdater registryOperationUpdater)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.registryOperationUpdater = registryOperationUpdater;
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(HostedServiceSettings.ConsumerCount);

            logger.LogInformation($"Started {GetType().Name} consumer with group {HostedServiceSettings.GroupId}");

            return eventReaderBuilder
                .OnEvent<RefundFromAccountablePersonCreated>(OnCreatedAsync)
                .OnEvent<RefundFromAccountablePersonUpdated>(OnUpdatedAsync)
                .OnEvent<RefundFromAccountablePersonDeleted>(OnDeletedAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(HostedServiceSettings.GroupId, cancellationToken);
        }

        private Task OnCreatedAsync(RefundFromAccountablePersonCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.CreateAsync(
                eventData.MapToCommand());
        }

        private Task OnUpdatedAsync(RefundFromAccountablePersonUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.UpdateAsync(
                eventData.MapToCommand());
        }

        private Task OnDeletedAsync(RefundFromAccountablePersonDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.DeleteAsync(
                eventData.MapToCommand());
        }
    }
}
