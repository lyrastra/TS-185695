using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Mappers.PurseOperations.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Money.ChangeLog.Extensions;

namespace Moedelo.Money.ChangeLog.HostedServices.PurseOperations.Outgoing
{
    public class TransferToSettlementAccountHostedService : BackgroundService
    {
        private static TransferToSettlementAccountStateDefinition StateDefinition =>
            TransferToSettlementAccountStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly ITransferToSettlementAccountEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly IPurseApiClient purseApiClient;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;

        public TransferToSettlementAccountHostedService(
            ILogger<TransferToSettlementAccountHostedService> logger,
            ITransferToSettlementAccountEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            IPurseApiClient purseApiClient,
            ISettlementAccountApiClient settlementAccountApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.purseApiClient = purseApiClient;
            this.settlementAccountApiClient = settlementAccountApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<TransferToSettlementAccountCreated>(OnCreated)
                .OnEvent<TransferToSettlementAccountUpdated>(OnUpdated)
                .OnEvent<TransferToSettlementAccountDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(TransferToSettlementAccountCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.PurseId,
                eventData.SettlementAccountId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Purse,
                    extraInfo.SettlementAccount),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(TransferToSettlementAccountUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.PurseId,
                eventData.SettlementAccountId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Purse,
                    extraInfo.SettlementAccount),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(TransferToSettlementAccountDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Deleted,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(),
                eventMetadata.MessageDate);
        }

        private async Task<ExtraStateInfo> GetExtraStateInfoAsync(
            int purseId,
            int? settlementAccountId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var purse = (await purseApiClient.GetAsync(
                executionContext.FirmId,
                executionContext.UserId))
                .FirstOrDefault(x => x.Id == purseId);

            var settlementAccount = settlementAccountId.HasValue
                ? await settlementAccountApiClient.GetByIdAsync(
                    executionContext.FirmId,
                    executionContext.UserId,
                    settlementAccountId.Value)
                : null;

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Purse = purse,
                SettlementAccount = settlementAccount
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public PurseDto Purse { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
        }
    }
}
