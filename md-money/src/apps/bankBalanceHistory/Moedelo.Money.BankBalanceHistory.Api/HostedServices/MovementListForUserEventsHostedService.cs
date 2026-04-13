using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.BankBalanceHistory.Api.Extensions;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Enums;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Events;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.ImportForUser.Events;

namespace Moedelo.Money.BankBalanceHistory.Api.HostedServices
{
    [InjectAsHostedService]
    public class MovementListForUserEventsHostedService: BackgroundService
    {
        private readonly SettingValue consumerCountSetting;
        private readonly ILogger<MovementListForUserEventsHostedService> logger;
        private readonly IMovementRequestForUserEventReaderBuilder movementRequestForUserEventReaderBuilder;
        private readonly IMovementHandler movementHandler;

        public MovementListForUserEventsHostedService(
            ISettingRepository settingRepository,
            ILogger<MovementListForUserEventsHostedService> logger, 
            IMovementRequestForUserEventReaderBuilder movementRequestForUserEventReaderBuilder, 
            IMovementHandler movementHandler)
        {
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
            this.logger = logger;
            this.movementRequestForUserEventReaderBuilder = movementRequestForUserEventReaderBuilder;
            this.movementHandler = movementHandler;
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);
            
            await movementRequestForUserEventReaderBuilder
                .OnImportForUserEvent(OnKafkaMessageAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithConsumerCount(consumerCount)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);
            
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnKafkaMessageAsync(ImportForUserKafkaEvent eventData)
        {
            return movementHandler.ProcessMovementAsync(
                eventData.FirmId,
                eventData.FileId,
                MovementListSourceType.FromUserImport
            );
        }
    }
}