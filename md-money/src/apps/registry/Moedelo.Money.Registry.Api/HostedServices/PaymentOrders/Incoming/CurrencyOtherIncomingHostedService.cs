using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyOther.Events;
using Moedelo.Money.Registry.Api.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Registry.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Registry.Api.HostedServices.PaymentOrders.Incoming
{
    public class CurrencyOtherIncomingHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ICurrencyOtherIncomingEventReader eventReader;
        private readonly IRegistryOperationUpdater registryOperationUpdater;
        private readonly SettingValue consumerCountSetting;

        public CurrencyOtherIncomingHostedService(
            ILogger<CurrencyOtherIncomingHostedService> logger,
            ISettingRepository settingRepository,
            ICurrencyOtherIncomingEventReader eventReader,
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

        private Task OnCreate(CurrencyOtherIncomingCreatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.CreateAsync(
                eventData.MapToCommand());
        }

        private Task OnUpdate(CurrencyOtherIncomingUpdatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.UpdateAsync(
                eventData.MapToCommand());
        }

        private Task OnDelete(CurrencyOtherIncomingDeletedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.DeleteAsync(
                eventData.MapToCommand());
        }
    }
}
