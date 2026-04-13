using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Extensions;
using Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Outgoing
{
    public class BudgetaryPaymentHostedService : BackgroundService
    {
        private static BudgetaryPaymentStateDefinition StateDefinition =>
            BudgetaryPaymentStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IBudgetaryPaymentEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;
        private readonly IKbkApiClient kbkApiClient;

        public BudgetaryPaymentHostedService(
            ILogger<BudgetaryPaymentHostedService> logger,
            IBudgetaryPaymentEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ICashApiClient cashApiClient,
            IKbkApiClient kbkApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.cashApiClient = cashApiClient;
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
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.KbkId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.KbkInfo),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(BudgetaryPaymentUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.KbkId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
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
            long cashId,
            int? kbkId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var cash = await cashApiClient.GetByIdAsync(
                executionContext.UserId,
                executionContext.FirmId,
                cashId);

            var kbkInfo = kbkId.HasValue
                ? await kbkApiClient.GetAsync(kbkId.Value)
                : null;


            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash,
                KbkInfo = kbkInfo
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
            public KbkDto KbkInfo { get; init; }
        }
    }
}
