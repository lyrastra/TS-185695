using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Entities;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Commands;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Handler.Mappers.PaymentOrders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ActualizeFromImport;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ChangeIsPaidFromIntegration;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders
{
    public class PaymentOrderOutgoingCommandHostedService : BackgroundService
    {
        private readonly SettingValue consumerCountSetting;
        private readonly ILogger<PaymentOrderOutgoingCommandHostedService> logger;
        private readonly IPaymentOrderOutgoingCommandReaderBuilder commandReaderBuilder;
        private readonly IPaymentOrderActualizer actualizer;
        private readonly IPaymentOrderDetailUpdater detailUpdater;

        public PaymentOrderOutgoingCommandHostedService(
            ILogger<PaymentOrderOutgoingCommandHostedService> logger,
            IPaymentOrderOutgoingCommandReaderBuilder commandReaderBuilder,
            IPaymentOrderActualizer actualizer,
            IPaymentOrderDetailUpdater detailUpdater,
            ISettingRepository settingRepository)
        {
            this.logger = logger;
            this.commandReaderBuilder = commandReaderBuilder;
            this.actualizer = actualizer;
            this.detailUpdater = detailUpdater;
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogInformation($"Starting consumer {GetType().Name} with group {MoneyConstants.GroupId}");
            return commandReaderBuilder
                .OnActualize(OnActualizeAsync)
                .OnChangeIsPaid(OnChangeIsPaidAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                // некоторые очень большие сообщения могут обрабатываться до 20 минут
                .WithOptionalSettings(new OptionalReadSettings{MaxPollIntervalMs = 60000 * 20})
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(2)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);
        }

        private Task OnActualizeAsync(ActualizeFromImport commandData)
        {
            var items = PaymentOrderMapper.Map(commandData);
            return actualizer.ActualizeAsync(items);
        }
        
        private Task OnChangeIsPaidAsync(ChangeIsPaidFromIntegrationItem commandData)
        {
            var item = PaymentOrderMapper.Map(commandData);
            return detailUpdater.UpdateAsync(item);
        }
    }
}