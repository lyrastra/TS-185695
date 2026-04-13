using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Helpers;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots;
using Moedelo.Money.PaymentOrders.Business.TaxationSystems;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.Domain.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Moedelo.Money.PaymentOrders.Business.Abstractions.KontragentSettlementAccounts;
using Microsoft.Extensions.Logging;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderCreator))]
    internal class PaymentOrderCreator : IPaymentOrderCreator
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly PaymentSnapshotBuilder snapshotBuilder;
        private readonly IPaymentOrderDao dao;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;
        private readonly IKontragentSettlementAccountsReader kontragentSettlementAccountsReader;
        private readonly ILogger<PaymentOrderCreator> logger;

        public PaymentOrderCreator(
            IExecutionInfoContextAccessor executionInfoContext,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            PaymentSnapshotBuilder snapshotBuilder,
            IPaymentOrderDao dao,
            IHistoricalLogsCommandWriter historicalLogsWriter,
            IKontragentSettlementAccountsReader kontragentSettlementAccountsReader,
            ILogger<PaymentOrderCreator> logger)
        {
            this.executionInfoContext = executionInfoContext;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.snapshotBuilder = snapshotBuilder;
            this.dao = dao;
            this.historicalLogsWriter = historicalLogsWriter;
            this.kontragentSettlementAccountsReader = kontragentSettlementAccountsReader;
            this.logger = logger;
        }

        public async Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            await CheckKontragentSettlementAccounts(request, (int)context.FirmId).ConfigureAwait(false);
            var paymentOrder = await GetPaymentOrder(request, (int)context.FirmId).ConfigureAwait(false);
            DirectionValidator(paymentOrder);
            paymentOrder.Id = await dao.InsertAsync(paymentOrder).ConfigureAwait(false);
            await historicalLogsWriter.WriteAsync(LogOperationType.Create, paymentOrder).ConfigureAwait(false);
            return new CreateResponse
            {
                Id = paymentOrder.Id,
                DocumentBaseId = paymentOrder.DocumentBaseId
            };
        }

        private async Task<PaymentOrder> GetPaymentOrder(PaymentOrderSaveRequest request, int firmId)
        {
            var paymentOrder = request.PaymentOrder;
            paymentOrder.Id = 0;
            paymentOrder.FirmId = firmId;
            paymentOrder.DocumentBaseId = request.DocumentBaseId;
            paymentOrder.Date = paymentOrder.Date.Date;
            paymentOrder.CreateDate = DateTime.Now;
            paymentOrder.ModifyDate = DateTime.Now;
            paymentOrder.KontragentName = StringLengthTruncater.Truncate(paymentOrder.KontragentName, 255);
            paymentOrder.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(paymentOrder.Date.Year).ConfigureAwait(false);
            paymentOrder.Description = StringLengthTruncater.Truncate(paymentOrder.Description, 500);
            paymentOrder.PaymentSnapshot = await snapshotBuilder.BuildAsync(request).ConfigureAwait(false);
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
