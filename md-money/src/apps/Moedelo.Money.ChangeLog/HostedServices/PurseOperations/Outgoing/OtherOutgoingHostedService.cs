using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.ChangeLog.Mappers.PurseOperations.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.Other;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.Other.Events;
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
    public class OtherOutgoingHostedService : BackgroundService
    {
        private static OtherOutgoingStateDefinition StateDefinition =>
            OtherOutgoingStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IOtherOutgoingEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly IPurseApiClient purseApiClient;
        private readonly IBaseDocumentsClient baseDocumentsApiClient;
        private readonly IContractsApiClient contractsApiClient;

        public OtherOutgoingHostedService(
            ILogger<OtherOutgoingHostedService> logger,
            IOtherOutgoingEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            IPurseApiClient purseApiClient,
            IBaseDocumentsClient baseDocumentsApiClient,
            IContractsApiClient contractsApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.purseApiClient = purseApiClient;
            this.baseDocumentsApiClient = baseDocumentsApiClient;
            this.contractsApiClient = contractsApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<OtherOutgoingCreated>(OnCreated)
                .OnEvent<OtherOutgoingUpdated>(OnUpdated)
                .OnEvent<OtherOutgoingDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(OtherOutgoingCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.PurseId,
                eventData.ContractBaseId,
                eventData.BillBaseId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.BillLink,
                    extraInfo.Purse,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(OtherOutgoingUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.PurseId,
                eventData.ContractBaseId,
                eventData.BillBaseId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.BillLink,
                    extraInfo.Purse,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(OtherOutgoingDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            long? contractBaseId,
            long? billBaseId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var purse = (await purseApiClient.GetAsync(
                executionContext.FirmId,
                executionContext.UserId))
                .FirstOrDefault(x => x.Id == purseId);

            var contract = contractBaseId.HasValue
                ? (await contractsApiClient.GetByBaseIdsAsync(
                        executionContext.FirmId,
                        executionContext.UserId,
                        new[] { contractBaseId.Value })).SingleOrDefault()
                : null;

            var billLink = billBaseId.HasValue
                ? await baseDocumentsApiClient.GetByIdAsync(billBaseId.Value)
                : null;

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Purse = purse,
                Contract = contract,
                BillLink = billLink
            };
        }

        private struct ExtraStateInfo
        {
            public BaseDocumentDto BillLink { get; init; }
            public ExecutionInfoContext ExecutionContext { get; init; }
            public PurseDto Purse { get; init; }
            public ContractDto Contract { get; init; }
            public PatentWithoutAdditionalDataDto Patent { get; set; }
        }
    }
}
