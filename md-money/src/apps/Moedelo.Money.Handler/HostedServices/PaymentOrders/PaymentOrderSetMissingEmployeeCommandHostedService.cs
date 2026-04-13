using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders.MissingEmployee;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders
{
    public class PaymentOrderSetMissingEmployeeCommandHostedService : BackgroundService
    {
        private readonly SettingValue consumerCountSetting;
        private readonly IPaymentOrderSetMissingEmployeeCommandReader reader;
        private readonly ILogger<PaymentOrderSetMissingEmployeeCommandHostedService> logger;
        private readonly IPaymentOrderWithMissingEmployeeUpdater updater;

        public PaymentOrderSetMissingEmployeeCommandHostedService(
            IPaymentOrderSetMissingEmployeeCommandReader reader,
            ILogger<PaymentOrderSetMissingEmployeeCommandHostedService> logger,
            IPaymentOrderWithMissingEmployeeUpdater updater,
            ISettingRepository settingRepository)
        {
            this.reader = reader;
            this.logger = logger;
            this.updater = updater;
            consumerCountSetting = settingRepository.Get("PaymentOrderSetMissingEmployeeCommandConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);
           
            await reader.ReadAsync(MoneyConstants.GroupId, OnChangeAsync, consumerCount: consumerCount, cancellationToken: cancellationToken);
        
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnChangeAsync(PaymentOrderSetMissingEmployeeCommandMessage message, KafkaMessageValueMetadata metadata)
        {
            return updater.UpdateAsync(message.EmployeeId, message.DocumentBaseIds);
        }
    }
}
