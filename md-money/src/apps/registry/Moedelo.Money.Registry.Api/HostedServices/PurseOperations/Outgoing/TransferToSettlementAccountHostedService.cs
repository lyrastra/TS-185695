using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount.Events;
using Moedelo.Money.Registry.Api.Mappers.PurseOperations.Outgoing;
using Moedelo.Money.Registry.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;

namespace Moedelo.Money.Registry.Api.HostedServices.PurseOperations.Outgoing
{
    public class TransferToSettlementAccountHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ITransferToSettlementAccountEventReaderBuilder eventReaderBuilder;
        private readonly IRegistryOperationUpdater registryOperationUpdater;
        private readonly SettingValue consumerCountSetting;

        public TransferToSettlementAccountHostedService(
            ILogger<TransferToSettlementAccountHostedService> logger,
            ISettingRepository settingRepository,
            ITransferToSettlementAccountEventReaderBuilder eventReaderBuilder,
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
                .OnEvent<TransferToSettlementAccountCreated>(OnCreatedAsync)
                .OnEvent<TransferToSettlementAccountUpdated>(OnUpdatedAsync)
                .OnEvent<TransferToSettlementAccountDeleted>(OnDeletedAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(HostedServiceSettings.GroupId, cancellationToken);
        }

        private Task OnCreatedAsync(TransferToSettlementAccountCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.CreateAsync(
                eventData.MapToCommand());
        }

        private Task OnUpdatedAsync(TransferToSettlementAccountUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.UpdateAsync(
                eventData.MapToCommand());
        }

        private Task OnDeletedAsync(TransferToSettlementAccountDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.DeleteAsync(
                eventData.MapToCommand());
        }
    }
}
