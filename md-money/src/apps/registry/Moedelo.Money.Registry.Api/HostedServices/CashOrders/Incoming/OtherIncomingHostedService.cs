using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.Other;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.Other.Events;
using Moedelo.Money.Registry.Api.Mappers.CashOrders.Incoming;
using Moedelo.Money.Registry.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;

namespace Moedelo.Money.Registry.Api.HostedServices.CashOrders.Incoming
{
    public class OtherIncomingHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IOtherIncomingEventReaderBuilder eventReaderBuilder;
        private readonly IRegistryOperationUpdater registryOperationUpdater;
        private readonly SettingValue consumerCountSetting;

        public OtherIncomingHostedService(
            ILogger<OtherIncomingHostedService> logger,
            ISettingRepository settingRepository,
            IOtherIncomingEventReaderBuilder eventReaderBuilder,
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
                .OnEvent<OtherIncomingCreated>(OnCreatedAsync)
                .OnEvent<OtherIncomingUpdated>(OnUpdatedAsync)
                .OnEvent<OtherIncomingDeleted>(OnDeletedAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(HostedServiceSettings.GroupId, cancellationToken);
        }

        private Task OnCreatedAsync(OtherIncomingCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.CreateAsync(
                eventData.MapToCommand());
        }

        private Task OnUpdatedAsync(OtherIncomingUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.UpdateAsync(
                eventData.MapToCommand());
        }

        private Task OnDeletedAsync(OtherIncomingDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.DeleteAsync(
                eventData.MapToCommand());
        }
    }
}
