using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Events;
using Moedelo.Money.Business.Abstractions.Operations.TaxationSystemChangingSync;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices
{
    public class TaxationSystemChangedEventHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ITaxationSystemChangedEventReader reader;
        private readonly ITaxationSystemChangingSyncObjectManager syncObjectManager;
        private readonly SettingValue consumerCountSetting;

        public TaxationSystemChangedEventHostedService(
            ILogger<TaxationSystemChangedEventHostedService> logger,
            ITaxationSystemChangedEventReader reader,
            ISettingRepository settingRepository,
            ITaxationSystemChangingSyncObjectManager syncObjectManager)
        {
            this.logger = logger;
            this.reader = reader;
            this.syncObjectManager = syncObjectManager;
            consumerCountSetting = settingRepository.Get("TaxationSystemChangedEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await reader.ReadAsync(MoneyConstants.GroupId, OnChangeAsync, consumerCount: consumerCount,
                cancellationToken: cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnChangeAsync(TaxationSystemChangedEvent message, KafkaMessageValueMetadata metadata)
        {
            return syncObjectManager.ChangeStateAsync(message.Guid, message.DocumentBaseId);
        }
    }
}
