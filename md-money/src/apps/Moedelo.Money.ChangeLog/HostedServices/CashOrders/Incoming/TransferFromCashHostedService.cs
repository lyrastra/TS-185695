using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
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
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Incoming
{
    public class TransferFromCashHostedService : BackgroundService
    {
        private static TransferFromCashStateDefinition StateDefinition => TransferFromCashStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly ITransferFromCashEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;

        public TransferFromCashHostedService(
            ILogger<TransferFromCashHostedService> logger,
            ITransferFromCashEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ICashApiClient cashApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.cashApiClient = cashApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<TransferFromCashCreated>(OnCreated)
                .OnEvent<TransferFromCashUpdated>(OnUpdated)
                .OnEvent<TransferFromCashDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(TransferFromCashCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.FromCashId);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.FromCash),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(TransferFromCashUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.FromCashId);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.FromCash),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(TransferFromCashDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            long fromCashId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var cash = await cashApiClient.GetByIdAsync(
                executionContext.UserId,
                executionContext.FirmId,
                cashId);

            var fromCash = await cashApiClient.GetByIdAsync(
                 executionContext.UserId,
                 executionContext.FirmId,
                 fromCashId);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash,
                FromCash = fromCash
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
            public CashDto FromCash { get; init; }
        }
    }
}
