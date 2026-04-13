using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing.Commands;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders
{
    public class PaymentOrdersBatchProvideCommandHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IPaymentOrdersBatchProvideCommandReaderBuilder commandReaderBuilder;
        private readonly IPaymentOrdersBatchProvideCommandHandler commandHandler;

        public PaymentOrdersBatchProvideCommandHostedService(
            ILogger<PaymentOrdersBatchProvideCommandHostedService> logger,
            IPaymentOrdersBatchProvideCommandReaderBuilder commandReaderBuilder,
            IPaymentOrdersBatchProvideCommandHandler commandHandler)
        {
            this.logger = logger;
            this.commandReaderBuilder = commandReaderBuilder;
            this.commandHandler = commandHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await commandReaderBuilder
                .OnProvide(OnProvideAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(6, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options =>
                {
                    options.UsePostgresMemoryStorage();
                    options.ErrorToleranceTimeSpan = TimeSpan.FromDays(1);
                    options.MaxPausedMessageKeys = 16;
                    options.MaxOffsetMapDepth = 32000;
                })
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnProvideAsync(PaymentOrdersBatchProvideCommand commandData)
        {
            return commandHandler.ProvideAsync(commandData.DocumentBaseIds, commandData.ProvidingStateId);
        }
    }
}