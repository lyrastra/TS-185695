using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions;
using Moedelo.Money.CashOrders.Business.Abstractions.HistoricalLogs;
using Moedelo.Money.CashOrders.Business.Abstractions.Models;
using Moedelo.Money.CashOrders.Business.AccPostings;
using Moedelo.Money.CashOrders.Business.CashOrders.Extensions;
using Moedelo.Money.CashOrders.Business.Kbks;
using Moedelo.Money.CashOrders.Business.TaxationSystems;
using Moedelo.Money.CashOrders.DataAccess.Abstractions;
using Moedelo.Money.CashOrders.Domain.Models;
using Moedelo.Money.Enums;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderUpdater))]
    internal class CashOrderUpdater : ICashOrderUpdater
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly SyntheticAccountReader syntheticAccountReader;
        private readonly ICashOrderReader getter;
        private readonly ICashOrderDao cashOrderDao;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;

        public CashOrderUpdater(
            IExecutionInfoContextAccessor executionInfoContext,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            SyntheticAccountReader syntheticAccountReader,
            ICashOrderReader getter,
            ICashOrderDao cashOrderDao,
            IHistoricalLogsCommandWriter historicalLogsWriter)
        {
            this.executionInfoContext = executionInfoContext;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.syntheticAccountReader = syntheticAccountReader;
            this.getter = getter;
            this.cashOrderDao = cashOrderDao;
            this.historicalLogsWriter = historicalLogsWriter;
        }

        public async Task UpdateAsync(CashOrderSaveRequest request)
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var cashOrder = await GetCashOrderAsync(request, (int)context.FirmId);
            cashOrder.SyntheticAccountTypeId = await syntheticAccountReader.GetIdByCodeAsync(cashOrder.GetSyntheticAccountCode());

            await cashOrderDao.UpdateAsync(cashOrder);

            await historicalLogsWriter.WriteAsync(LogOperationType.Update, cashOrder);
        }

        private async Task<CashOrder> GetCashOrderAsync(CashOrderSaveRequest request, int firmId)
        {
            // новая модель, которая будет записана в БД
            var cashOrder = request.CashOrder;
            // старая модель, которая уже находится в БД
            var oldCashOrderResponse = await getter.GetByBaseIdAsync(request.DocumentBaseId, cashOrder.OperationType);
            var oldCashOrder = oldCashOrderResponse.CashOrder;
            // записываем в новую модель те поля, которые не должны измениться при обновлении
            cashOrder.Id = oldCashOrder.Id;
            cashOrder.FirmId = firmId;
            cashOrder.DocumentBaseId = oldCashOrder.DocumentBaseId;
            //cashOrder.OperationType = oldCashOrder.OperationType;
            cashOrder.KbkId = oldCashOrder.KbkId;
            cashOrder.AccountCode = oldCashOrder.AccountCode;
            cashOrder.Direction = oldCashOrder.Direction;
            cashOrder.CreateDate = oldCashOrder.CreateDate;
            cashOrder.ModifyDate = DateTime.Now;
            cashOrder.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(cashOrder.Date.Year);
            if (cashOrder.SalaryWorkerId == 0)
            {
                cashOrder.SalaryWorkerId = null;
            }
            return cashOrder;
        }
    }
}
