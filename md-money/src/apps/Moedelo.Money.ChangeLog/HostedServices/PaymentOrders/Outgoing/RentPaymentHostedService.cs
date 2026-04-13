using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Extensions;
using Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Events;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.ChangeLog.HostedServices.PaymentOrders.Outgoing
{
    public class RentPaymentHostedService : BackgroundService
    {
        private static RentPaymentStateDefinition StateDefinition =>
            RentPaymentStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IRentPaymentEventReader eventReader;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly IContractsApiClient contractsApiClient;
        private readonly IInventoryCardApiClient inventoryCardApiClient;
        private readonly SettingValue consumerCountSetting;

        public RentPaymentHostedService(
            ILogger<RentPaymentHostedService> logger,
            ISettingRepository settingRepository,
            IRentPaymentEventReader eventReader,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient,
            IContractsApiClient contractsApiClient,
            IInventoryCardApiClient inventoryCardApiClient)
        {
            this.logger = logger;
            this.eventReader = eventReader;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.contractsApiClient = contractsApiClient;
            this.inventoryCardApiClient = inventoryCardApiClient;
            consumerCountSetting = settingRepository.Get("ChangeLogEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(HostedServiceSettings.ConsumerCount);

            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReader.ReadAsync(Application.GroupId, OnCreate, OnUpdate, OnDelete, consumerCount: consumerCount, cancellationToken: cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreate(RentPaymentCreatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.InventoryCardBaseId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.Contract,
                    extraInfo.InventoryCard),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdate(RentPaymentUpdatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.InventoryCardBaseId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.Contract,
                    extraInfo.InventoryCard),
                eventMetadata.MessageDate);
        }

        private Task OnDelete(RentPaymentDeletedMessage eventData, KafkaMessageValueMetadata eventMetadata)
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
            long? inventoryCardBaseId)
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

            var inventoryCard = inventoryCardBaseId.HasValue
                ? (await inventoryCardApiClient.GetByBaseIdsAsync(
                        executionContext.FirmId,
                        executionContext.UserId,
                        new[] { inventoryCardBaseId.Value })).SingleOrDefault()
                : null;

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                SettlementAccount = settlementAccount,
                Contract = contract,
                InventoryCard = inventoryCard
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
            public ContractDto Contract { get; set; }
            public InventoryCardDto InventoryCard { get; set; }
        }
    }
}
