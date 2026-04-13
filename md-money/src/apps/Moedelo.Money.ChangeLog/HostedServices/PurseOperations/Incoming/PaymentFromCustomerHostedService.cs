using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Incoming;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.ChangeLog.Mappers.PurseOperations.Incoming;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Incoming.PaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Incoming.PaymentFromCustomer.Events;
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

namespace Moedelo.Money.ChangeLog.HostedServices.PurseOperations.Incoming
{
    public class PaymentFromCustomerHostedService : BackgroundService
    {
        private static PaymentFromCustomerStateDefinition StateDefinition =>
            PaymentFromCustomerStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IPaymentFromCustomerEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly IPurseApiClient purseApiClient;
        private readonly IBaseDocumentsClient baseDocumentsApiClient;
        private readonly IContractsApiClient contractsApiClient;
        private readonly IPatentApiClient patentApiClient;
        private readonly ITaxationSystemApiClient taxationSystemApiClient;

        public PaymentFromCustomerHostedService(
            ILogger<PaymentFromCustomerHostedService> logger,
            IPaymentFromCustomerEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            IPurseApiClient purseApiClient,
            IBaseDocumentsClient baseDocumentsApiClient,
            IContractsApiClient contractsApiClient,
            IPatentApiClient patentApiClient,
            ITaxationSystemApiClient taxationSystemApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.purseApiClient = purseApiClient;
            this.baseDocumentsApiClient = baseDocumentsApiClient;
            this.contractsApiClient = contractsApiClient;
            this.patentApiClient = patentApiClient;
            this.taxationSystemApiClient = taxationSystemApiClient;
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
                eventData.PurseId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.BillBaseId,
                eventData.DocumentLinks,
                eventData.Date.Year);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.Purse,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.IsOsno),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(PaymentFromCustomerUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.PurseId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.BillBaseId,
                eventData.DocumentLinks,
                eventData.Date.Year);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.Purse,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.IsOsno),
                eventMetadata.MessageDate);
        }

        private async Task OnProvided(PaymentFromCustomerProvided eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.PurseId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.BillBaseId,
                eventData.DocumentLinks,
                eventData.Date.Year);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.Purse,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.IsOsno),
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
            int purseId,
            long? contractBaseId,
            long? patentId,
            long? billBaseId,
            IEnumerable<DocumentLink> eventLinkedDocuments,
            int year)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var purse = (await purseApiClient.GetAsync(
                executionContext.FirmId,
                executionContext.UserId))
                .FirstOrDefault(x => x.Id == purseId);

            var contract = contractBaseId.HasValue
                ? (await contractsApiClient.GetByBaseIdsAsync(
                        executionContext.FirmId,
                        executionContext.UserId,
                        new[] { contractBaseId.Value })).SingleOrDefault()
                : null;

            var patent = patentId.HasValue && patentId.Value != 0
                ? await patentApiClient.GetWithoutAdditionalDataByIdAsync(
                        executionContext.FirmId,
                        executionContext.UserId,
                        patentId.Value)
                : null;

            var baseIds = (eventLinkedDocuments
                    ?.Select(link => link.DocumentBaseId)
                    .ToArray() ?? Array.Empty<long>())
                .Distinct()
                .ToList();

            if (billBaseId.HasValue && billBaseId > 0)
            {
                baseIds.Add(billBaseId.Value);
            }

            var linkedDocuments = await baseDocumentsApiClient.GetByIdsAsync(baseIds);

            var linkedDocumentsMap = linkedDocuments
                .ToDictionary(link => link.Id);

            var taxationSystem = await taxationSystemApiClient.GetByYearAsync(
                executionContext.FirmId,
                executionContext.UserId,
                year);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Purse = purse,
                Contract = contract,
                Patent = patent,
                LinkedDocumentsMap = linkedDocumentsMap,
                IsOsno = taxationSystem?.IsOsno ?? false
            };
        }

        private struct ExtraStateInfo
        {
            public IReadOnlyDictionary<long, BaseDocumentDto> LinkedDocumentsMap { get; init; }
            public ExecutionInfoContext ExecutionContext { get; init; }
            public PurseDto Purse { get; init; }
            public ContractDto Contract { get; init; }
            public PatentWithoutAdditionalDataDto Patent { get; init; }
            public bool IsOsno { get; init; }
        }
    }
}
