using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Money.ChangeLog.Extensions;

namespace Moedelo.Money.ChangeLog.HostedServices.PaymentOrders.Incoming
{
    public class MediationFeeHostedService : BackgroundService
    {
        private static MediationFeeStateDefinition StateDefinition =>
            MediationFeeStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IMediationFeeEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly IBaseDocumentsClient baseDocumentsApiClient;
        private readonly IContractsApiClient contractsApiClient;

        public MediationFeeHostedService(
            ILogger<MediationFeeHostedService> logger,
            IMediationFeeEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient,
            IBaseDocumentsClient baseDocumentsApiClient,
            IContractsApiClient contractsApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.baseDocumentsApiClient = baseDocumentsApiClient;
            this.contractsApiClient = contractsApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<MediationFeeCreated>(OnCreated)
                .OnEvent<MediationFeeUpdated>(OnUpdated)
                .OnEvent<MediationFeeProvideRequired>(OnProvideRequired)
                .OnEvent<MediationFeeDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(MediationFeeCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.OperationState.IsBadOperationState())
            {
                // не показываем нераспознанные операции
                return;
            }

            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.DocumentLinks,
                eventData.BillLinks);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.SettlementAccount,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(MediationFeeUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.DocumentLinks,
                eventData.BillLinks);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.SettlementAccount,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private async Task OnProvideRequired(MediationFeeProvideRequired eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.DocumentLinks,
                eventData.BillLinks);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.SettlementAccount,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(MediationFeeDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Deleted,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(),
                eventMetadata.MessageDate);
        }

        private async Task<ExtraStateInfo> GetExtraStateInfoAsync(
            int settlementAccountId,
            long contractBaseId,
            IEnumerable<DocumentLink> eventLinkedDocuments,
            IEnumerable<BillLink> linkedBills)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var settlementAccount = await settlementAccountApiClient.GetByIdAsync(
                executionContext.FirmId,
                executionContext.UserId,
                settlementAccountId);

            var contract = contractBaseId > 0
                ? (await contractsApiClient.GetByBaseIdsAsync(
                    executionContext.FirmId,
                    executionContext.UserId,
                    new[] { contractBaseId })).Single()
                : null;

            var baseIds = (eventLinkedDocuments
                    ?.Select(link => link.DocumentBaseId)
                    .ToArray() ?? Array.Empty<long>())
                .Concat(linkedBills
                    ?.Select(link => link.BillBaseId)
                    .ToArray() ?? Array.Empty<long>())
                .Distinct()
                .ToArray();

            var linkedDocuments = await baseDocumentsApiClient.GetByIdsAsync(baseIds);

            var linkedDocumentsMap = linkedDocuments
                .ToDictionary(link => link.Id);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                SettlementAccount = settlementAccount,
                Contract = contract,
                LinkedDocumentsMap = linkedDocumentsMap
            };
        }

        private struct ExtraStateInfo
        {
            public IReadOnlyDictionary<long, BaseDocumentDto> LinkedDocumentsMap { get; init; }
            public ExecutionInfoContext ExecutionContext { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
            public ContractDto Contract { get; init; }
        }
    }
}
