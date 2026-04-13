using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
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
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Events;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.ChangeLog.HostedServices.PaymentOrders.Outgoing
{
    public class CurrencyBankFeeHostedService : BackgroundService
    {
        private static CurrencyBankFeeStateDefinition StateDefinition =>
            CurrencyBankFeeStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly ICurrencyBankFeeEventReader eventReader;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly SettingValue consumerCountSetting;

        public CurrencyBankFeeHostedService(
            ILogger<CurrencyBankFeeHostedService> logger,
            ISettingRepository settingRepository,
            ICurrencyBankFeeEventReader eventReader,
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

        private async Task OnCreate(CurrencyBankFeeCreatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.OperationState.IsBadOperationState())
            {
                // не показываем нераспознанные операции
                return;
            }

            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdate(CurrencyBankFeeUpdatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount),
                eventMetadata.MessageDate);
        }

        private Task OnDelete(CurrencyBankFeeDeletedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Deleted,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(),
                eventMetadata.MessageDate);
        }

        private async Task<ExtraStateInfo> GetExtraStateInfoAsync(int settlementAccountId)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            var settlementAccount = await settlementAccountApiClient.GetByIdAsync(
                executionContext.FirmId,
                executionContext.UserId,
                settlementAccountId);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                SettlementAccount = settlementAccount
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
        }
    }
}
