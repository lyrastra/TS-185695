using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Events;
using Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Registry.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Registry.Api.HostedServices.PaymentOrders.Outgoing
{
    public class CurrencyPaymentToSupplierHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ICurrencyPaymentToSupplierEventReader eventReader;
        private readonly IRegistryOperationUpdater registryOperationUpdater;
        private readonly SettingValue consumerCountSetting;

        public CurrencyPaymentToSupplierHostedService(
            ILogger<CurrencyPaymentToSupplierHostedService> logger,
            ISettingRepository settingRepository,
            ICurrencyPaymentToSupplierEventReader eventReader,
            IRegistryOperationUpdater registryOperationUpdater)
        {
            this.logger = logger;
            this.eventReader = eventReader;
            this.registryOperationUpdater = registryOperationUpdater;
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(HostedServiceSettings.ConsumerCount);

            logger.LogInformation($"Started {GetType().Name} consumer with group {HostedServiceSettings.GroupId}");

            return eventReader.ReadAsync(HostedServiceSettings.GroupId, OnCreate, OnUpdate, OnDelete, consumerCount: consumerCount, cancellationToken: cancellationToken);
        }

        private Task OnCreate(CurrencyPaymentToSupplierCreatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.OperationState.IsBadOperationState())
            {
                // не показываем нераспознанные операции
                return Task.CompletedTask;
            }
            return registryOperationUpdater.CreateAsync(
                eventData.MapToCommand());
        }

        private Task OnUpdate(CurrencyPaymentToSupplierUpdatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.UpdateAsync(
                eventData.MapToCommand());
        }

        private Task OnDelete(CurrencyPaymentToSupplierDeletedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.DeleteAsync(
                eventData.MapToCommand());
        }
    }
}