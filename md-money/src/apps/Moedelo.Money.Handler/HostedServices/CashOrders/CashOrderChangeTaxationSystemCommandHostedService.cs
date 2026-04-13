using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.CashOrders;
using Moedelo.Money.Business.Abstractions.Commands.CashOrders;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices.CashOrders
{
    public class CashOrderChangeTaxationSystemCommandHostedService : BackgroundService
    {
        private readonly SettingValue consumerCountSetting;
        private readonly ICashOrderChangeTaxationSystemCommandReader reader;
        private readonly ILogger<CashOrderChangeTaxationSystemCommandHostedService> logger;
        private readonly ICashOrderTaxationSystemUpdater updater;

        public CashOrderChangeTaxationSystemCommandHostedService(
            ICashOrderChangeTaxationSystemCommandReader reader,
            ILogger<CashOrderChangeTaxationSystemCommandHostedService> logger,
            ICashOrderTaxationSystemUpdater updater,
            ISettingRepository settingRepository)
        {
            this.reader = reader;
            this.logger = logger;
            this.updater = updater;
            consumerCountSetting = settingRepository.Get("CashOrderChangeTaxationSystemCommandConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);
           
            await reader.ReadAsync(MoneyConstants.GroupId, OnChangeAsync, consumerCount: consumerCount, cancellationToken: cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnChangeAsync(CashOrderChangeTaxationSystemCommandMessage message, KafkaMessageValueMetadata metadata)
        {
            return updater.UpdateAsync(message.DocumentBaseId, message.TaxationSystemType, message.Guid);
        }
    }
}
