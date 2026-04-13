using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.Money.ChangeLog.Extensions;
using Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Events;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System.Collections.Generic;

namespace Moedelo.Money.ChangeLog.HostedServices.PaymentOrders.Outgoing
{
    public class CurrencyPaymentToSupplierHostedService : BackgroundService
    {
        private static CurrencyPaymentToSupplierStateDefinition StateDefinition =>
            CurrencyPaymentToSupplierStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly ICurrencyPaymentToSupplierEventReader eventReader;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly IBaseDocumentsClient baseDocumentsApiClient;
        private readonly IContractsApiClient contractsApiClient;
        private readonly SettingValue consumerCountSetting;

        public CurrencyPaymentToSupplierHostedService(
            ILogger<CurrencyPaymentToSupplierHostedService> logger,
            ISettingRepository settingRepository,
            ICurrencyPaymentToSupplierEventReader eventReader,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient,
            IBaseDocumentsClient baseDocumentsApiClient,
            IContractsApiClient contractsApiClient)
        {
            this.logger = logger;
            this.eventReader = eventReader;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.baseDocumentsApiClient = baseDocumentsApiClient;
            this.contractsApiClient = contractsApiClient;
            consumerCountSetting = settingRepository.Get("ChangeLogEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(HostedServiceSettings.ConsumerCount);

            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReader.ReadAsync(Application.GroupId, OnCreate, OnUpdate, OnDelete, consumerCount: consumerCount, cancellationToken: cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreate(CurrencyPaymentToSupplierCreatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.OperationState.IsBadOperationState())
            {
                // не показываем нераспознанные операции
                return;
            }

            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.DocumentLinks);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdate(CurrencyPaymentToSupplierUpdatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.DocumentLinks);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.LinkedDocumentsMap,
                    extraInfo.Contract),
                eventMetadata.MessageDate);
        }

        private Task OnDelete(CurrencyPaymentToSupplierDeletedMessage eventData, KafkaMessageValueMetadata eventMetadata)
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
            long? contractBaseId,
            IEnumerable<DocumentLink> eventLinkedDocuments)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var settlementAccount = await settlementAccountApiClient.GetByIdAsync(
                executionContext.FirmId,
                executionContext.UserId,
                settlementAccountId);

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
                SettlementAccount = settlementAccount,
                LinkedDocumentsMap = linkedDocumentsMap,
                Contract = contract
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
            public IReadOnlyDictionary<long, BaseDocumentDto> LinkedDocumentsMap { get; init; }
            public ContractDto Contract { get; init; }
        }
    }
}
