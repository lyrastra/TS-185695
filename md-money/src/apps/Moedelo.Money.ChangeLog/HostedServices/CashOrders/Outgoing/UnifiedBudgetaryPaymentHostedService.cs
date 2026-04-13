using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Money.ChangeLog.Extensions;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Outgoing
{
    public class UnifiedBudgetaryPaymentHostedService : BackgroundService
    {
        private static UnifiedBudgetaryPaymentStateDefinition StateDefinition =>
            UnifiedBudgetaryPaymentStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IUnifiedBudgetaryPaymentEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;
        private readonly IKbkApiClient kbkApiClient;
        private readonly IPatentApiClient patentApiClient;

        public UnifiedBudgetaryPaymentHostedService(
            ILogger<UnifiedBudgetaryPaymentHostedService> logger,
            IUnifiedBudgetaryPaymentEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ICashApiClient cashApiClient,
            IKbkApiClient kbkApiClient,
            IPatentApiClient patentApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.cashApiClient = cashApiClient;
            this.kbkApiClient = kbkApiClient;
            this.patentApiClient = patentApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<UnifiedBudgetaryPaymentCreated>(OnCreated)
                .OnEvent<UnifiedBudgetaryPaymentUpdated>(OnUpdated)
                .OnEvent<UnifiedBudgetaryPaymentDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(UnifiedBudgetaryPaymentCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.SubPayments.Select(x => x.KbkId).ToArray(),
                eventData.SubPayments.Where(x => x.PatentId.HasValue).Select(x => x.PatentId.Value).ToArray());

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.KbkMap,
                    extraInfo.PatentMap),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(UnifiedBudgetaryPaymentUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.SubPayments.Select(x => x.KbkId).ToArray(),
                eventData.SubPayments.Where(x => x.PatentId.HasValue).Select(x => x.PatentId.Value).ToArray());

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.KbkMap,
                    extraInfo.PatentMap),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(UnifiedBudgetaryPaymentDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            IReadOnlyCollection<int> kbkIds,
            IReadOnlyCollection<long> patentIds)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var cash = await cashApiClient.GetByIdAsync(
                executionContext.UserId,
                executionContext.FirmId,
                cashId);

            var kbks = await kbkApiClient.GetByIdsAsync(kbkIds);

            var patents = await patentApiClient.GetWithoutAdditionalDataByIdsAsync(
                executionContext.FirmId,
                executionContext.UserId,
                patentIds);


            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash,
                KbkMap = kbks.ToDictionary(x => x.Id),
                PatentMap = patents.ToDictionary(x => x.Id)
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
            public IReadOnlyDictionary<int, KbkDto> KbkMap { get; init; }
            public IReadOnlyDictionary<long, PatentWithoutAdditionalDataDto> PatentMap { get; init; }
        }
    }
}
