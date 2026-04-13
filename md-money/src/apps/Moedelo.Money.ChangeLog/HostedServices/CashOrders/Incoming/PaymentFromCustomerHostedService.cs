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
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.Money.ChangeLog.Extensions;
using Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Incoming
{
    public class PaymentFromCustomerHostedService : BackgroundService
    {
        private static PaymentFromCustomerStateDefinition StateDefinition =>
            PaymentFromCustomerStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IPaymentFromCustomerEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;
        private readonly IContractsApiClient contractsApiClient;
        private readonly IPatentApiClient patentApiClient;
        private readonly IBaseDocumentsClient baseDocumentsApiClient;

        public PaymentFromCustomerHostedService(
            ILogger<PaymentFromCustomerHostedService> logger,
            IPaymentFromCustomerEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ICashApiClient cashApiClient,
            IContractsApiClient contractsApiClient,
            IPatentApiClient patentApiClient,
            IBaseDocumentsClient baseDocumentsApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.cashApiClient = cashApiClient;
            this.contractsApiClient = contractsApiClient;
            this.patentApiClient = patentApiClient;
            this.baseDocumentsApiClient = baseDocumentsApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<PaymentFromCustomerCreated>(OnCreated)
                .OnEvent<PaymentFromCustomerUpdated>(OnUpdated)
                .OnEvent<PaymentFromCustomerProvided>(OnProvided)
                .OnEvent<PaymentFromCustomerDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(PaymentFromCustomerCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.BillLinks,
                eventData.DocumentLinks);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.LinkedDocumentsMap),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(PaymentFromCustomerUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.BillLinks,
                eventData.DocumentLinks);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.LinkedDocumentsMap),
                eventMetadata.MessageDate);
        }

        private async Task OnProvided(PaymentFromCustomerProvided eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.BillLinks,
                eventData.DocumentLinks);
            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.LinkedDocumentsMap),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(PaymentFromCustomerDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            long? patentId,
            IReadOnlyCollection<BillLink> linkedBills,
            IReadOnlyCollection<DocumentLink> linkedDocuments)
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

            var patent = patentId.HasValue && patentId > 0
                ? await patentApiClient.GetWithoutAdditionalDataByIdAsync(
                        executionContext.FirmId,
                        executionContext.UserId,
                        patentId.Value)
                : null;

            var baseIds = (linkedDocuments
                    ?.Select(link => link.DocumentBaseId)
                    .ToArray() ?? Array.Empty<long>())
                .Concat(linkedBills
                    ?.Select(link => link.BillBaseId)
                    .ToArray() ?? Array.Empty<long>())
                .Distinct()
                .ToArray();

            var baseDocuments = await baseDocumentsApiClient.GetByIdsAsync(baseIds);

            var linkedDocumentsMap = baseDocuments
                .ToDictionary(link => link.Id);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash,
                Contract = contract,
                Patent = patent,
                LinkedDocumentsMap = linkedDocumentsMap
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
            public ContractDto Contract { get; init; }
            public PatentWithoutAdditionalDataDto Patent { get; set; }
            public IReadOnlyDictionary<long, BaseDocumentDto> LinkedDocumentsMap { get; init; }
        }
    }
}
