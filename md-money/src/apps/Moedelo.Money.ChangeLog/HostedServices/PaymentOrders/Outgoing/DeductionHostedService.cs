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
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Money.ChangeLog.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Events;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Money.ChangeLog.HostedServices.PaymentOrders.Outgoing
{
    public class DeductionHostedService : BackgroundService
    {
        private static DeductionStateDefinition StateDefinition =>
            DeductionStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IDeductionEventReaderBuilder eventReaderBuilder;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly IContractsApiClient contractsApiClient;
        private readonly IEmployeesApiClient employeesApiClient;

        public DeductionHostedService(
            ILogger<DeductionHostedService> logger,
            IDeductionEventReaderBuilder eventReaderBuilder,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ISettlementAccountApiClient settlementAccountApiClient,
            IContractsApiClient contractsApiClient,
            IEmployeesApiClient employeesApiClient)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.contractsApiClient = contractsApiClient;
            this.employeesApiClient = employeesApiClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder
                .OnEvent<DeductionCreated>(OnCreated)
                .OnEvent<DeductionUpdated>(OnUpdated)
                .OnEvent<DeductionDeleted>(OnDeleted)
                .SkipIfUnknownType()
                .WithFallbackOffset(HostedServiceSettings.FallbackOffset)
                .WithRetry(HostedServiceSettings.RetrySettings)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(Application.GroupId, cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreated(DeductionCreated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            if (eventData.OperationState.IsBadOperationState())
            {
                // не показываем нераспознанные операции
                return;
            }

            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.DeductionWorkerId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.Contract,
                    extraInfo.DeductionWorker),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdated(DeductionUpdated eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.SettlementAccountId,
                eventData.ContractBaseId,
                eventData.DeductionWorkerId);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.SettlementAccount,
                    extraInfo.Contract,
                    extraInfo.DeductionWorker),
                eventMetadata.MessageDate);
        }

        private Task OnDeleted(DeductionDeleted eventData, KafkaMessageValueMetadata eventMetadata)
        {
            return changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Deleted,
                StateDefinition,
                executionInfoContextAccessor.ExecutionInfoContext,
                eventData.MapToState(),
                eventMetadata.MessageDate);
        }

        private async Task<ExtraStateInfo> GetExtraStateInfoAsync(int settlementAccountId,
            long? contractBaseId, int? deductionWorkerId)
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

            var deductionWorker = deductionWorkerId.HasValue
                ? (await employeesApiClient.GetByIdAsync(
                    executionContext.FirmId,
                    executionContext.UserId,
                    deductionWorkerId.Value))
                : null;

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                SettlementAccount = settlementAccount,
                Contract = contract,
                DeductionWorker = deductionWorker,
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public SettlementAccountDto SettlementAccount { get; init; }
            public ContractDto Contract { get; init; }
            public WorkerDto DeductionWorker { get; set; }
        }
    }
}
