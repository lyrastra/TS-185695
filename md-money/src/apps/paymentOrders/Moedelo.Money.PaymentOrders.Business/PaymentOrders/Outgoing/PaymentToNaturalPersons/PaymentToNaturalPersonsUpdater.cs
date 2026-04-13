using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Snapshots;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsUpdater))]
    class PaymentToNaturalPersonsUpdater : IPaymentToNaturalPersonsUpdater
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IPaymentOrderReader paymentOrderReader;
        private readonly IPaymentOrderUpdater paymentOrderUpdater;
        private readonly PaymentSnapshotBuilder snapshotBuilder;
        private readonly IPaymentOrderDao paymentOrderDao;
        private readonly IWorkerPaymentDao workerPaymentDao;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;

        public PaymentToNaturalPersonsUpdater(
            IExecutionInfoContextAccessor contextAccessor,
            IPaymentOrderReader paymentOrderReader,
            IPaymentOrderUpdater paymentOrderUpdater,
            PaymentSnapshotBuilder snapshotBuilder,
            IPaymentOrderDao paymentOrderDao,
            IWorkerPaymentDao workerPaymentDao,
            IHistoricalLogsCommandWriter historicalLogsWriter)
        {
            this.contextAccessor = contextAccessor;
            this.paymentOrderReader = paymentOrderReader;
            this.paymentOrderUpdater = paymentOrderUpdater;
            this.snapshotBuilder = snapshotBuilder;
            this.paymentOrderDao = paymentOrderDao;
            this.workerPaymentDao = workerPaymentDao;
            this.historicalLogsWriter = historicalLogsWriter;
        }

        public async Task UpdateAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            if (request.PaymentOrder.UnderContract.IsSalaryProject()
                || request.PaymentOrder.OperationState.IsBadOperationState())
            {
                await UpdateSalaryProjectPaymentAsync(request);
                return;
            }

            await paymentOrderUpdater.UpdateAsync(request);
            await workerPaymentDao.OverwriteAsync((int)context.FirmId, request.DocumentBaseId, request.ChargePayments);
        }

        // todo: пока для ЗП проекта можно менять только номер, дату и признак оплаты
        private async Task UpdateSalaryProjectPaymentAsync(PaymentOrderSaveRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            // старая модель, которую нужно обновить
            var oldPaymentOrderResponse = await paymentOrderReader.GetByBaseIdAsync(request.DocumentBaseId, OperationType.PaymentOrderOutgoingPaymentToNaturalPersons);
            var oldPaymentOrder = oldPaymentOrderResponse.PaymentOrder;
            // старые начисления
            var oldWorkerPayments = await workerPaymentDao.GetByBaseIdAsync((int)context.FirmId, request.DocumentBaseId);
            // записываем в старую модель те поля, которые должны измениться при обновлении
            oldPaymentOrder.Number = request.PaymentOrder.Number;
            oldPaymentOrder.Date = request.PaymentOrder.Date;
            oldPaymentOrder.PaidStatus = request.PaymentOrder.PaidStatus;
            oldPaymentOrder.ProvideInAccounting = request.PaymentOrder.ProvideInAccounting;
            oldPaymentOrder.TaxPostingType = request.PaymentOrder.TaxPostingType;
            oldPaymentOrder.ModifyDate = DateTime.Now;
            // для снапшота нужно использовать старую обновленную модель
            var requestFromOldModel = new PaymentOrderSaveRequest
            {
                PaymentOrder = oldPaymentOrder,
                ChargePayments = oldWorkerPayments
            };
            oldPaymentOrder.PaymentSnapshot = await snapshotBuilder.BuildAsync(requestFromOldModel);
            oldPaymentOrder.OperationState = request.PaymentOrder.OperationState;
            oldPaymentOrder.OutsourceState = request.PaymentOrder.OutsourceState;
            // сохраняем изменения в бд и в historical log
            await paymentOrderDao.UpdateAsync(oldPaymentOrder);
            await historicalLogsWriter.WriteAsync(LogOperationType.Update, oldPaymentOrder);
        }
    }
}
