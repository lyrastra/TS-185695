using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders
{
    public class PaymentOrderChangeTaxationSystemCommandHostedService : BackgroundService
    {
        private readonly SettingValue consumerCountSetting;
        private readonly IPaymentOrderChangeTaxationSystemCommandReader reader;
        private readonly ILogger<PaymentOrderChangeTaxationSystemCommandHostedService> logger;
        private readonly IPaymentOrderTaxationSystemUpdater updater;

        public PaymentOrderChangeTaxationSystemCommandHostedService(
            IPaymentOrderChangeTaxationSystemCommandReader reader,
            ILogger<PaymentOrderChangeTaxationSystemCommandHostedService> logger,
            IPaymentOrderTaxationSystemUpdater updater,
            ISettingRepository settingRepository)
        {
            this.reader = reader;
            this.logger = logger;
            this.updater = updater;
            consumerCountSetting = settingRepository.Get("PaymentOrderChangeTaxationSystemCommandConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await reader.ReadAsync(MoneyConstants.GroupId, OnChangeAsync, consumerCount: consumerCount,
                cancellationToken: cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnChangeAsync(PaymentOrderChangeTaxationSystemCommandMessage message,
            KafkaMessageValueMetadata metadata)
        {
            return updater.UpdateAsync(message.DocumentBaseId, message.TaxationSystemType, message.Guid);
        }
    }
}
