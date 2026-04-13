using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.RefundToCustomer.Events;
using Moedelo.Money.Registry.Api.Mappers.CashOrders.Outgoing;
using Moedelo.Money.Registry.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Registry.Api.HostedServices.CashOrders.Outgoing
{
    public class RefundToCustomerHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IRefundToCustomerEventReader eventReader;
        private readonly IRegistryOperationUpdater registryOperationUpdater;
        private readonly SettingValue consumerCountSetting;

        public RefundToCustomerHostedService(
            ILogger<RefundToCustomerHostedService> logger,
            ISettingRepository settingRepository,
            IRefundToCustomerEventReader eventReader,
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

        private Task OnCreate(RefundToCustomerCreatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.CreateAsync(
                eventData.MapToCommand());
        }

        private Task OnUpdate(RefundToCustomerUpdatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.UpdateAsync(
                eventData.MapToCommand());
        }

        private Task OnDelete(RefundToCustomerDeletedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return registryOperationUpdater.DeleteAsync(
                eventData.MapToCommand());
        }
    }
}
