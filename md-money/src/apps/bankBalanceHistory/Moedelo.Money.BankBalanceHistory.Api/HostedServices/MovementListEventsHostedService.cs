using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest;
using Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest.Events;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Enums;
using Moedelo.Money.BankBalanceHistory.Api.Extensions;

namespace Moedelo.Money.BankBalanceHistory.Api.HostedServices
{
    [InjectAsHostedService]
    public class MovementListEventsHostedService : BackgroundService
    {

        private readonly SettingValue consumerCountSetting;
        private readonly IMovementRequestEventReaderBuilder movementRequestEventReaderBuilder;
        private readonly ILogger<MovementListEventsHostedService> logger;
        private readonly IMovementHandler movementHandler;

        public MovementListEventsHostedService(
            IMovementRequestEventReaderBuilder movementRequestEventReaderBuilder,
            ILogger<MovementListEventsHostedService> logger,
            ISettingRepository settingRepository,
            IMovementHandler movementHandler)
        {
            this.movementRequestEventReaderBuilder = movementRequestEventReaderBuilder;
            this.logger = logger;
            this.movementHandler = movementHandler;
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await movementRequestEventReaderBuilder
                .OnRequestFinished(OnMovementRequestFinishedAsync)
                .OnReviseRequestFinished(OnReviseMovementRequestFinishedAsync)
                .OnSilentRequestFinished(OnSilentMovementRequestFinishedAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithConsumerCount(consumerCount)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }


        private Task OnMovementRequestFinishedAsync(MovementRequestEventData eventData)
        {
            return movementHandler.ProcessMovementAsync(
                eventData.FirmId,
                eventData.MongoFileId,
                MovementListSourceType.FromIntegration
            );
        }

        private Task OnReviseMovementRequestFinishedAsync(ReviseMovementRequestEventData eventData)
        {
            return movementHandler.ProcessMovementAsync(
                eventData.FirmId,
                eventData.FileId,
                MovementListSourceType.FromIntegrationReconcilationTempFile
            );
        }

        private Task OnSilentMovementRequestFinishedAsync(SilentMovementRequestEventData eventData)
        {
            return movementHandler.ProcessMovementAsync(
                eventData.FirmId,
                eventData.MongoFileId,
                MovementListSourceType.FromIntegration
            );
        }
    }
}
