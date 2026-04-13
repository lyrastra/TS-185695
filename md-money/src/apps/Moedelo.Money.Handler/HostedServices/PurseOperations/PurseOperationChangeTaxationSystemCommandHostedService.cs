using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Commands.PurseOperations;
using Moedelo.Money.Business.Abstractions.PurseOperations;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices.PurseOperations
{
    public class PurseOperationChangeTaxationSystemCommandHostedService : BackgroundService
    {
        private readonly SettingValue consumerCountSetting;
        private readonly IPurseOperationChangeTaxationSystemCommandReader reader;
        private readonly ILogger<PurseOperationChangeTaxationSystemCommandHostedService> logger;
        private readonly IPurseOperationTaxationSystemUpdater updater;

        public PurseOperationChangeTaxationSystemCommandHostedService(
            IPurseOperationChangeTaxationSystemCommandReader reader,
            ILogger<PurseOperationChangeTaxationSystemCommandHostedService> logger,
            IPurseOperationTaxationSystemUpdater updater,
            ISettingRepository settingRepository)
        {
            this.reader = reader;
            this.logger = logger;
            this.updater = updater;
            consumerCountSetting = settingRepository.Get("PurseOperationChangeTaxationSystemCommandConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);
           
            await reader.ReadAsync(MoneyConstants.GroupId, OnChangeAsync, consumerCount: consumerCount, cancellationToken: cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnChangeAsync(PurseOperationChangeTaxationSystemCommandMessage message, KafkaMessageValueMetadata metadata)
        {
            return updater.UpdateAsync(message.DocumentBaseId, message.TaxationSystemType, message.Guid);
        }
    }
}
