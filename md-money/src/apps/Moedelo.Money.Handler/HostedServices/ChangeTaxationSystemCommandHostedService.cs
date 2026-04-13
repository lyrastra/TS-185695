using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Commands;
using Moedelo.Money.Business.Abstractions.Operations;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices
{
    public class ChangeTaxationSystemCommandHostedService : BackgroundService
    {
        private readonly SettingValue consumerCountSetting;
        private readonly IChangeTaxationSystemCommandReader reader;
        private readonly ILogger<ChangeTaxationSystemCommandHostedService> logger;
        private readonly IChangeTaxationSystemSender sender;

        public ChangeTaxationSystemCommandHostedService(
            IChangeTaxationSystemCommandReader reader,
            ILogger<ChangeTaxationSystemCommandHostedService> logger,
            ISettingRepository settingRepository,
            IChangeTaxationSystemSender sender)
        {
            this.reader = reader;
            this.logger = logger;
            this.sender = sender;
            consumerCountSetting = settingRepository.Get("ChangeTaxationSystemCommandConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await reader.ReadAsync(MoneyConstants.GroupId, OnChangeAsync, consumerCount: consumerCount,
                cancellationToken: cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnChangeAsync(ChangeTaxationSystemCommand message, KafkaMessageValueMetadata metadata)
        {
            return sender.DistributeCommandsAsync(message.DocumentBaseIds, message.TaxationSystemType, message.Guid);
        }
    }
}
