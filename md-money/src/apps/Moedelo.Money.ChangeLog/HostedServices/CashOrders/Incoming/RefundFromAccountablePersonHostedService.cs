using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Extensions;
using Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson;
using System.Threading.Tasks;
using System.Threading;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Incoming
{
    public class RefundFromAccountablePersonHostedService : BackgroundService
    {
        private static RefundFromAccountablePersonStateDefinition StateDefinition =>
            RefundFromAccountablePersonStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IRefundFromAccountablePersonEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;
        private readonly IAdvanceStatementApiClient advanceStatementApiClient;

        public RefundFromAccountablePersonHostedService(
            ILogger<RefundFromAccountablePersonHostedService> logger,
            IRefundFromAccountablePersonEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ICashApiClient cashApiClient,
            IAdvanceStatementApiClient advanceStatementApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.cashApiClient = cashApiClient;
            this.advanceStatementApiClient = advanceStatementApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);
            await eventReaderBuilder
                .OnEvent<RefundFromAccountablePersonCreated>(OnCreated)
                .OnEvent<RefundFromAccountablePersonUpdated>(OnUpdated)
                .OnEvent<RefundFromAccountablePersonProvided>(OnProvided)
                .OnEvent<RefundFromAccountablePersonDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(RefundFromAccountablePersonCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.AdvanceStatementBaseId);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.AdvanceStatement),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(RefundFromAccountablePersonUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.AdvanceStatementBaseId);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.AdvanceStatement),
                eventMetadata.MessageDate);
        }

        private async Task OnProvided(RefundFromAccountablePersonProvided eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.AdvanceStatementBaseId);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.AdvanceStatement),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(RefundFromAccountablePersonDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Deleted,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(),
                eventMetadata.MessageDate);
        }

        private async Task<ExtraStateInfo> GetExtraStateInfoAsync(
            long cashId,
            long? advanceStatementBaseId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var cash = await cashApiClient.GetByIdAsync(
                executionContext.UserId,
                executionContext.FirmId,
                cashId);

            var advanceStatement = advanceStatementBaseId.HasValue && advanceStatementBaseId > 0
                ? await advanceStatementApiClient.GetByBaseIdAsync(executionContext.FirmId, executionContext.UserId, advanceStatementBaseId.Value)
                : null;

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash,
                AdvanceStatement = advanceStatement
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
            public AdvanceStatementDto AdvanceStatement { get; init; }
        }
    }
}
