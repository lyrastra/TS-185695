using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToAccountablePerson.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Money.ChangeLog.Extensions;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Outgoing
{
    public class PaymentToAccountablePersonHostedService : BackgroundService
    {
        private static PaymentToAccountablePersonStateDefinition StateDefinition =>
            PaymentToAccountablePersonStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IPaymentToAccountablePersonEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;
        private readonly ILinksClient linksClient;

        public PaymentToAccountablePersonHostedService(
            ILogger<PaymentToAccountablePersonHostedService> logger,
            IPaymentToAccountablePersonEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ICashApiClient cashApiClient,
            ILinksClient linksClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.cashApiClient = cashApiClient;
            this.linksClient = linksClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<PaymentToAccountablePersonCreated>(OnCreated)
                .OnEvent<PaymentToAccountablePersonUpdated>(OnUpdated)
                .OnEvent<PaymentToAccountablePersonProvided>(OnProvided)
                .OnEvent<PaymentToAccountablePersonDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(PaymentToAccountablePersonCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.AdvanceStatementBaseIds?.Any() == true)
            {
                await Task.Delay(3000); // костыль, ждем для корректного отображения связей
            }

            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.DocumentBaseId,
                eventData.AdvanceStatementBaseIds);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.AdvanceStatementMap),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(PaymentToAccountablePersonUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.DocumentBaseId,
                eventData.AdvanceStatementBaseIds);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.AdvanceStatementMap),
                eventMetadata.MessageDate);
        }

        private async Task OnProvided(PaymentToAccountablePersonProvided eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.DocumentBaseId,
                eventData.AdvanceStatementBaseIds);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.AdvanceStatementMap),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(PaymentToAccountablePersonDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            long documentBaseId,
            IReadOnlyCollection<long> advanceStatementBaseIds)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var cash = await cashApiClient.GetByIdAsync(
                executionContext.UserId,
                executionContext.FirmId,
                cashId);

            var advanceStatementLinks = (await linksClient.GetLinksWithDocumentsAsync(documentBaseId))
                .Where(x => advanceStatementBaseIds.Contains(x.Document.Id))
                .ToArray();

            var advanceStatementMap = advanceStatementLinks.ToDictionary(x => x.Document.Id);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash,
                AdvanceStatementMap = advanceStatementMap
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
            public IReadOnlyDictionary<long, LinkWithDocumentDto> AdvanceStatementMap { get; init; }
        }
    }
}
