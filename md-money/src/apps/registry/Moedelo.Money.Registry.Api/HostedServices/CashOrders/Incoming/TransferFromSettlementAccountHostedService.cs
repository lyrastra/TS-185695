using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromSettlementAccount;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromSettlementAccount.Events;
using Moedelo.Money.Registry.Api.Mappers.CashOrders.Incoming;
using Moedelo.Money.Registry.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;

namespace Moedelo.Money.Registry.Api.HostedServices.CashOrders.Incoming
{
    public class TransferFromSettlementAccountHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ITransferFromSettlementAccountEventReaderBuilder eventReaderBuilder;
        private readonly IRegistryOperationUpdater registryOperationUpdater;
        private readonly SettingValue consumerCountSetting;

        public TransferFromSettlementAccountHostedService(
            ILogger<TransferFromSettlementAccountHostedService> logger,
            ISettingRepository settingRepository,
            ITransferFromSettlementAccountEventReaderBuilder eventReaderBuilder,
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
                .OnEvent<TransferFromSettlementAccountCreated>(OnCreatedAsync)
                .OnEvent<TransferFromSettlementAccountUpdated>(OnUpdatedAsync)
                .OnEvent<TransferFromSettlementAccountDeleted>(OnDeletedAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(HostedServiceSettings.GroupId, cancellationToken);
        }

        private Task OnCreatedAsync(TransferFromSettlementAccountCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.CreateAsync(
                eventData.MapToCommand());
        }

        private Task OnUpdatedAsync(TransferFromSettlementAccountUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.UpdateAsync(
                eventData.MapToCommand());
        }

        private Task OnDeletedAsync(TransferFromSettlementAccountDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.DeleteAsync(
                eventData.MapToCommand());
        }
    }
}
