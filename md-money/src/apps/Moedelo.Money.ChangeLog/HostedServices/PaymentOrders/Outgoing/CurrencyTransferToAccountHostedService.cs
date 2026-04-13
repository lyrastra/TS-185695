using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Extensions;
using Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Events;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.ChangeLog.HostedServices.PaymentOrders.Outgoing
{
    public class CurrencyTransferToAccountHostedService : BackgroundService
    {
        private static CurrencyTransferToAccountStateDefinition StateDefinition =>
            CurrencyTransferToAccountStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly ICurrencyTransferToAccountEventReader eventReader;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly SettingValue consumerCountSetting;

        public CurrencyTransferToAccountHostedService(
            ILogger<CurrencyTransferToAccountHostedService> logger,
            ISettingRepository settingRepository,
            ICurrencyTransferToAccountEventReader eventReader,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient)
        {
            this.logger = logger;
            this.eventReader = eventReader;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
            consumerCountSetting = settingRepository.Get("ChangeLogEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(HostedServiceSettings.ConsumerCount);

            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReader.ReadAsync(Application.GroupId, OnCreate, OnUpdate, OnDelete, consumerCount: consumerCount, cancellationToken: cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreate(CurrencyTransferToAccountCreatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.OperationState.IsBadOperationState())
            {
                // не показываем нераспознанные операции
                return;
            }

            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ToSettlementAccountId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.ToSettlementAccount),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdate(CurrencyTransferToAccountUpdatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ToSettlementAccountId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.ToSettlementAccount),
                eventMetadata.MessageDate);
        }

        private Task OnDelete(CurrencyTransferToAccountDeletedMessage eventData, KafkaMessageValueMetadata eventMetadata)
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
            int? toSettlementAccountId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var settlementAccounts = await settlementAccountApiClient.GetByIdsAsync(
                executionContext.FirmId,
                executionContext.UserId,
                new []{settlementAccountId, toSettlementAccountId ?? 0});

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                SettlementAccount = settlementAccounts.FirstOrDefault(x => x.Id == settlementAccountId),
                ToSettlementAccount = settlementAccounts.FirstOrDefault(x => x.Id == toSettlementAccountId)
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
            public SettlementAccountDto ToSettlementAccount { get; init; }
        }
    }
}
