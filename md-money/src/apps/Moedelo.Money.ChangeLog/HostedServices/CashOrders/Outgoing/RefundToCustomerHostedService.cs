using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.ChangeLog.Extensions;
using Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.RefundToCustomer.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.RefundToCustomer;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.ChangeLog.HostedServices.CashOrders.Outgoing
{
    public class RefundToCustomerHostedService : BackgroundService
    {
        private static RefundToCustomerStateDefinition StateDefinition =>
            RefundToCustomerStateDefinition.Instance;

        private readonly ILogger logger;
        private readonly IRefundToCustomerEventReader eventReader;
        private readonly IChangeLogCommandWriter changeLogCommandWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly ICashApiClient cashApiClient;
        private readonly IContractsApiClient contractsApiClient;
        private readonly IPatentApiClient patentApiClient;
        private readonly ITaxationSystemApiClient taxationSystemApiClient;
        private readonly SettingValue consumerCountSetting;

        public RefundToCustomerHostedService(
            ILogger<RefundToCustomerHostedService> logger,
            ISettingRepository settingRepository,
            IRefundToCustomerEventReader eventReader,
            IChangeLogCommandWriter changeLogCommandWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor,
            ICashApiClient cashApiClient,
            IContractsApiClient contractsApiClient,
            IPatentApiClient patentApiClient,
            ITaxationSystemApiClient taxationSystemApiClient)
        {
            this.logger = logger;
            this.eventReader = eventReader;
            this.changeLogCommandWriter = changeLogCommandWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.cashApiClient = cashApiClient;
            this.contractsApiClient = contractsApiClient;
            this.patentApiClient = patentApiClient;
            this.taxationSystemApiClient = taxationSystemApiClient;
            consumerCountSetting = settingRepository.Get("ChangeLogEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(HostedServiceSettings.ConsumerCount);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReader.ReadAsync(Application.GroupId, OnCreate, OnUpdate, OnDelete, consumerCount: consumerCount, cancellationToken: cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreate(RefundToCustomerCreatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.Date.Year);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Created,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.IsOsno),
                eventMetadata.MessageDate);
        }

        private async Task OnUpdate(RefundToCustomerUpdatedMessage eventData, KafkaMessageValueMetadata eventMetadata)
        {
            var extraInfo = await GetExtraStateInfoAsync(
                eventData.CashId,
                eventData.ContractBaseId,
                eventData.PatentId,
                eventData.Date.Year);

            await changeLogCommandWriter.WriteAsync(
                ChangeLogEventType.Updated,
                StateDefinition,
                extraInfo.ExecutionContext,
                eventData.MapToState(
                    extraInfo.Cash,
                    extraInfo.Contract,
                    extraInfo.Patent,
                    extraInfo.IsOsno),
                eventMetadata.MessageDate);
        }

        private Task OnDelete(RefundToCustomerDeletedMessage eventData, KafkaMessageValueMetadata eventMetadata)
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
            int year)
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

            var taxationSystem = await taxationSystemApiClient.GetByYearAsync(
                executionContext.FirmId,
                executionContext.UserId,
                year);

            return new ExtraStateInfo
            {
                ExecutionContext = executionContext,
                Cash = cash,
                Contract = contract,
                Patent = patent,
                IsOsno = taxationSystem?.IsOsno ?? false
            };
        }

        private struct ExtraStateInfo
        {
            public ExecutionInfoContext ExecutionContext { get; init; }
            public CashDto Cash { get; init; }
            public ContractDto Contract { get; init; }
            public PatentWithoutAdditionalDataDto Patent { get; set; }
            public bool IsOsno { get; init; }
        }
    }
}
