using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PurseOperation;
using Moedelo.Accounting.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PurseOperations.Common;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Common.Commands;

namespace Moedelo.Money.Handler.HostedServices.PurseOperations
{
    public class PurseOperationCommandHostedService : BackgroundService
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ILogger logger;
        private readonly IPurseOperationCommandReaderBuilder commandReaderBuilder;
        private readonly IPurseOperationApiClient purseOperationClient;
        private readonly SettingValue consumerCountSetting;

        public PurseOperationCommandHostedService(IExecutionInfoContextAccessor contextAccessor,
            ILogger<PurseOperationCommandHostedService> logger,
            IPurseOperationCommandReaderBuilder commandReaderBuilder,
            IPurseOperationApiClient purseOperationClient,
            ISettingRepository settingRepository)
        {
            this.contextAccessor = contextAccessor;
            this.logger = logger;
            this.commandReaderBuilder = commandReaderBuilder;
            this.purseOperationClient = purseOperationClient;
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await commandReaderBuilder
                .OnCreate(OnCreateAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreateAsync(CreatePurseOperation commandData)
        {
            var request = new PurseOperationForMultipleTypesDto
            {
                Date = commandData.Date.ToString("dd.MM.yyyy"),
                PurseId = commandData.PurseId,
                PurseOperationType = (PurseOperationType)commandData.PurseOperationType,
                Sum = commandData.Sum,
                Comment = commandData.Comment,
                SettlementAccountId = commandData.SettlementAccountId ?? int.MinValue,
                KontragentId = commandData.KontragentId,
                TaxationSystemType = (TaxationSystemType?)commandData.TaxationSystemType,
                IncludeNds = commandData.IncludeNds,
                NdsSum = commandData.NdsSum,
                NdsType = commandData.NdsType != null ? (AccountingNdsType?)commandData.NdsType : null,
            };
            var context = contextAccessor.ExecutionInfoContext;
            await purseOperationClient.SavePurseOperationWithTypeAsync(context.FirmId, context.UserId, request);
        }
    }
}
