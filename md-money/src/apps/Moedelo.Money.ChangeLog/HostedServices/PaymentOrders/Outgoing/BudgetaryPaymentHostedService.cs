using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events;
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

namespace Moedelo.Money.ChangeLog.HostedServices.PaymentOrders.Outgoing
{
    public class BudgetaryPaymentHostedService : BackgroundService
    {
        private static BudgetaryPaymentStateDefinition StateDefinition =>
            BudgetaryPaymentStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IBudgetaryPaymentEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly IBaseDocumentsClient baseDocumentsApiClient;
        private readonly IKbkApiClient kbkApiClient;

        public BudgetaryPaymentHostedService(
            ILogger<BudgetaryPaymentHostedService> logger,
            IBudgetaryPaymentEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient,
            IBaseDocumentsClient baseDocumentsApiClient,
            IKbkApiClient kbkApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.baseDocumentsApiClient = baseDocumentsApiClient;
            this.kbkApiClient = kbkApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<BudgetaryPaymentCreated>(OnCreated)
                .OnEvent<BudgetaryPaymentUpdated>(OnUpdated)
                .OnEvent<BudgetaryPaymentDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(BudgetaryPaymentCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.OperationState.IsBadOperationState())
            {
                // не показываем нераспознанные операции
                return;
            }

            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.CurrencyInvoicesLinks,
                eventData.KbkId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.KbkInfo),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(BudgetaryPaymentUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.CurrencyInvoicesLinks,
                eventData.KbkId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.KbkInfo),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(BudgetaryPaymentDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            IEnumerable<DocumentLink> eventLinkedDocuments,
            int? kbkId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var settlementAccount = await settlementAccountApiClient.GetByIdAsync(
                executionContext.FirmId,
                executionContext.UserId,
                settlementAccountId);

            var linkedDocuments = await baseDocumentsApiClient
                .GetByIdsAsync(eventLinkedDocuments
                    ?.Select(link => link.DocumentBaseId)
                    .ToArray() ?? Array.Empty<long>());

            var linkedDocumentsMap = linkedDocuments
                .ToDictionary(link => link.Id);

            var kbkInfo = kbkId.HasValue ? await kbkApiClient.GetAsync(kbkId.Value) : null;

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                SettlementAccount = settlementAccount,
                LinkedDocumentsMap = linkedDocumentsMap,
                KbkInfo = kbkInfo
            };
        }

        private struct ExtraStateInfo
        {
            public IReadOnlyDictionary<long, BaseDocumentDto> LinkedDocumentsMap { get; init; }
            public ExecutionInfoContext ExecutionContext { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
            public KbkDto KbkInfo { get; init; }
        }
    }
}
