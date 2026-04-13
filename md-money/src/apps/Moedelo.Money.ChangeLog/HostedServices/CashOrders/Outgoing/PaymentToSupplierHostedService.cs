using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Money.ChangeLog.Extensions;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Outgoing
{
    public class PaymentToSupplierHostedService : BackgroundService
    {
        private static PaymentToSupplierStateDefinition StateDefinition =>
            PaymentToSupplierStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IPaymentToSupplierEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;
        private readonly IBaseDocumentsClient baseDocumentsApiClient;
        private readonly IContractsApiClient contractsApiClient;

        public PaymentToSupplierHostedService(
            ILogger<PaymentToSupplierHostedService> logger,
            IPaymentToSupplierEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ICashApiClient cashApiClient,
            IBaseDocumentsClient baseDocumentsApiClient,
            IContractsApiClient contractsApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.cashApiClient = cashApiClient;
            this.baseDocumentsApiClient = baseDocumentsApiClient;
            this.contractsApiClient = contractsApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<PaymentToSupplierCreated>(OnCreated)
                .OnEvent<PaymentToSupplierUpdated>(OnUpdated)
                .OnEvent<PaymentToSupplierProvided>(OnProvided)
                .OnEvent<PaymentToSupplierDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(PaymentToSupplierCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.ContractBaseId,
                eventData.DocumentLinks);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(PaymentToSupplierUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.ContractBaseId,
                eventData.DocumentLinks);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private async Task OnProvided(PaymentToSupplierProvided eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.ContractBaseId,
                eventData.DocumentLinks);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(PaymentToSupplierDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            long? contractBaseId,
            IEnumerable<DocumentLink> eventLinkedDocuments)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var cash = await cashApiClient.GetByIdAsync(
                executionContext.UserId,
                executionContext.FirmId,
                cashId);

            var contract = contractBaseId.HasValue
                ? (await contractsApiClient.GetByBaseIdsAsync(
                        executionContext.FirmId,
                        executionContext.UserId,
                        new[] { contractBaseId.Value })).SingleOrDefault()
                : null;

            var linkedDocuments = await baseDocumentsApiClient
                .GetByIdsAsync(eventLinkedDocuments
                    ?.Select(link => link.DocumentBaseId)
                    .ToArray() ?? Array.Empty<long>());

            var linkedDocumentsMap = linkedDocuments
                .ToDictionary(link => link.Id);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash,
                LinkedDocumentsMap = linkedDocumentsMap,
                Contract = contract
            };
        }

        private struct ExtraStateInfo
        {
            public IReadOnlyDictionary<long, BaseDocumentDto> LinkedDocumentsMap { get; init; }
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
            public ContractDto Contract { get; init; }
        }
    }
}
