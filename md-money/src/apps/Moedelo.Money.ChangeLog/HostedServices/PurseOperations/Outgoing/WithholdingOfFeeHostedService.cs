using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Mappers.PurseOperations.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee.Events;
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
    public class WithholdingOfFeeHostedService : BackgroundService
    {
        private static WithholdingOfFeeStateDefinition StateDefinition =>
            WithholdingOfFeeStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IWithholdingOfFeeEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly IPurseApiClient purseApiClient;
        private readonly IPatentApiClient patentApiClient;

        public WithholdingOfFeeHostedService(
            ILogger<WithholdingOfFeeHostedService> logger,
            IWithholdingOfFeeEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            IPurseApiClient purseApiClient,
            IPatentApiClient patentApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.purseApiClient = purseApiClient;
            this.patentApiClient = patentApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<WithholdingOfFeeCreated>(OnCreated)
                .OnEvent<WithholdingOfFeeUpdated>(OnUpdated)
                .OnEvent<WithholdingOfFeeDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(WithholdingOfFeeCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.PurseId,
                eventData.PatentId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Purse,
                    extraInfo.Patent),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(WithholdingOfFeeUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.PurseId,
                eventData.PatentId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Purse,
                    extraInfo.Patent),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(WithholdingOfFeeDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            long? patentId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var purse = (await purseApiClient.GetAsync(
                executionContext.FirmId,
                executionContext.UserId))
                .FirstOrDefault(x => x.Id == purseId);

            var patent = patentId.HasValue && patentId.Value != 0
                ? await patentApiClient.GetWithoutAdditionalDataByIdAsync(
                        executionContext.FirmId,
                        executionContext.UserId,
                        patentId.Value)
                : null;

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Purse = purse,
                Patent = patent
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public PurseDto Purse { get; init; }
            public PatentWithoutAdditionalDataDto Patent { get; init; }
        }
    }
}
