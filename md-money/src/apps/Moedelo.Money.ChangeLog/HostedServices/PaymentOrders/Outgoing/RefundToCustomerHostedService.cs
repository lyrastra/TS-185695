using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Money.ChangeLog.Extensions;

namespace Moedelo.Money.ChangeLog.HostedServices.PaymentOrders.Outgoing
{
    public class RefundToCustomerHostedService : BackgroundService
    {
        private static RefundToCustomerStateDefinition StateDefinition =>
            RefundToCustomerStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IRefundToCustomerEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly IContractsApiClient contractsApiClient;
        private readonly IPatentApiClient patentApiClient;
        private readonly ITaxationSystemApiClient taxationSystemApiClient;

        public RefundToCustomerHostedService(
            ILogger<RefundToCustomerHostedService> logger,
            IRefundToCustomerEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient,
            IContractsApiClient contractsApiClient,
            IPatentApiClient patentApiClient,
            ITaxationSystemApiClient taxationSystemApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.contractsApiClient = contractsApiClient;
            this.patentApiClient = patentApiClient;
            this.taxationSystemApiClient = taxationSystemApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<RefundToCustomerCreated>(OnCreated)
                .OnEvent<RefundToCustomerUpdated>(OnUpdated)
                .OnEvent<RefundToCustomerDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(RefundToCustomerCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.OperationState.IsBadOperationState())
            {
                // не показываем нераспознанные операции
                return;
            }

            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.Date.Year);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.IsOsno),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(RefundToCustomerUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.Date.Year);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.IsOsno),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(RefundToCustomerDeleted eventData, KafkaMessageValueMetadata eventMetadata)
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
            long? patentId,
            int year)
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

            var patent = patentId.HasValue
                ? await patentApiClient.GetWithoutAdditionalDataByIdAsync(
                        executionContext.FirmId,
                        executionContext.UserId,
                        patentId.Value)
                : null;

            var taxationSystem = await taxationSystemApiClient.GetByYearAsync(
                executionContext.FirmId,
                executionContext.UserId,
                year);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                SettlementAccount = settlementAccount,
                Contract = contract,
                Patent = patent,
                IsOsno = taxationSystem?.IsOsno ?? false
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
            public ContractDto Contract { get; init; }
            public PatentWithoutAdditionalDataDto Patent { get; set; }
            public bool IsOsno { get; init; }
        }
    }
}
