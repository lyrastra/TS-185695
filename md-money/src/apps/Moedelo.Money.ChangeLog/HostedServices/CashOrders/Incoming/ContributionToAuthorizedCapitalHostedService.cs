using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionToAuthorizedCapital.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionToAuthorizedCapital;
using System.Threading.Tasks;
using System.Threading;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Incoming
{
    public class ContributionToAuthorizedCapitalHostedService : BackgroundService
    {
        private static ContributionToAuthorizedCapitalStateDefinition StateDefinition => ContributionToAuthorizedCapitalStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IContributionToAuthorizedCapitalEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;

        public ContributionToAuthorizedCapitalHostedService(
            ILogger<ContributionToAuthorizedCapitalHostedService> logger,
            IContributionToAuthorizedCapitalEventReaderBuilder eventReaderBuilder,
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
                .OnEvent<ContributionToAuthorizedCapitalCreated>(OnCreated)
                .OnEvent<ContributionToAuthorizedCapitalUpdated>(OnUpdated)
                .OnEvent<ContributionToAuthorizedCapitalDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(ContributionToAuthorizedCapitalCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(eventData.CashId);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(extraInfo.Cash),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(ContributionToAuthorizedCapitalUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(eventData.CashId);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(extraInfo.Cash),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(ContributionToAuthorizedCapitalDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Deleted,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(),
                eventMetadata.MessageDate);
        }

        private async Task<ExtraStateInfo> GetExtraStateInfoAsync(long cashId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var cash = await cashApiClient.GetByIdAsync(
                executionContext.UserId,
                executionContext.FirmId,
                cashId);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
        }
    }
}
