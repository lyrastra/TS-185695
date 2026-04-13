using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots;
using Moedelo.Money.PaymentOrders.Business.TaxationSystems;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models;
using System;
using System.Threading.Tasks;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Helpers;
using System.Linq;
using Moedelo.Money.PaymentOrders.Business.Abstractions.KontragentSettlementAccounts;
using Microsoft.Extensions.Logging;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Extensions;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderUpdater))]
    internal class PaymentOrderUpdater : IPaymentOrderUpdater
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly PaymentSnapshotBuilder snapshotBuilder;
        private readonly IPaymentOrderReader getter;
        private readonly IPaymentOrderDao dao;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;
        private readonly IKontragentSettlementAccountsReader kontragentSettlementAccountsReader;
        private readonly ILogger<PaymentOrderUpdater> logger;

        public PaymentOrderUpdater(
            IExecutionInfoContextAccessor executionInfoContext,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            PaymentSnapshotBuilder snapshotBuilder,
            IPaymentOrderReader getter,
            IPaymentOrderDao dao,
            IHistoricalLogsCommandWriter historicalLogsWriter,
            IKontragentSettlementAccountsReader kontragentSettlementAccountsReader,
            ILogger<PaymentOrderUpdater> logger)
        {
            this.executionInfoContext = executionInfoContext;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.snapshotBuilder = snapshotBuilder;
            this.getter = getter;
            this.dao = dao;
            this.historicalLogsWriter = historicalLogsWriter;
            this.kontragentSettlementAccountsReader = kontragentSettlementAccountsReader;
            this.logger = logger;
        }

        public async Task UpdateAsync(PaymentOrderSaveRequest request)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            await CheckKontragentSettlementAccounts(request, (int)context.FirmId);
            var paymentOrder = await GetPaymentOrderAsync(request);
            DirectionValidator(paymentOrder);
            await dao.UpdateAsync(paymentOrder);
            await historicalLogsWriter.WriteAsync(LogOperationType.Update, paymentOrder);
        }

        private async Task<PaymentOrder> GetPaymentOrderAsync(PaymentOrderSaveRequest request)
        {
            // новая модель, которая будет записана в БД
            var paymentOrder = request.PaymentOrder;
            // старая модель, которая уже находится в БД
            var oldPaymentOrderResponse = await getter.GetByBaseIdAsync(request.DocumentBaseId, paymentOrder.OperationType);
            var oldPaymentOrder = oldPaymentOrderResponse.PaymentOrder;
            // записываем в новую модель те поля, которые не должны измениться при обновлении
            paymentOrder.Id = oldPaymentOrder.Id;
            paymentOrder.FirmId = (int)executionInfoContext.ExecutionInfoContext.FirmId;
            paymentOrder.DocumentBaseId = oldPaymentOrder.DocumentBaseId;
            paymentOrder.OperationType = oldPaymentOrder.OperationType;
            paymentOrder.Direction = oldPaymentOrder.Direction;
            paymentOrder.SourceFileId = oldPaymentOrder.SourceFileId;
            paymentOrder.CreateDate = oldPaymentOrder.CreateDate;
            paymentOrder.ModifyDate = DateTime.Now;
            paymentOrder.KontragentName = StringLengthTruncater.Truncate(paymentOrder.KontragentName, 255);
            if (paymentOrder.TaxationSystemType == null)
            {
                paymentOrder.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(paymentOrder.Date.Year);
            }
            paymentOrder.Description = StringLengthTruncater.Truncate(paymentOrder.Description, 500);
            paymentOrder.PaymentSnapshot = await snapshotBuilder.BuildAsync(request);

            return paymentOrder;
        }

        private static void DirectionValidator(PaymentOrder paymentOrder)
        {
            if (!Enum.IsDefined(typeof(MoneyDirection), paymentOrder.Direction))
            {
                throw new ArgumentOutOfRangeException("Direction", paymentOrder.Direction, "Value not defined in enum, " + nameof(MoneyDirection));
            }
        }

        private async Task CheckKontragentSettlementAccounts(PaymentOrderSaveRequest request, int firmId)
        {
            if (request.PaymentOrder.KontragentId == null)
            {
                return;
            }
            var kontragentSettlementAccounts = await kontragentSettlementAccountsReader.GetByKontragentAsync((int)request.PaymentOrder.KontragentId);
            if (kontragentSettlementAccounts.Any() && string.IsNullOrEmpty(request.KontragentRequisites.SettlementAccount))
            {
                logger.LogWarning($"У контрагента есть привязанные р/счета, но был получен пустой р/с. baseId={request.DocumentBaseId}; firmId={firmId}; kontragentId={request.PaymentOrder.KontragentId}"); ;
            }
        }
    }
}
