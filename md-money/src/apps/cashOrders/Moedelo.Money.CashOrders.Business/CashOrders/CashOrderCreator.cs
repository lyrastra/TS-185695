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
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.CashOrders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderCreator))]
    internal class CashOrderCreator : ICashOrderCreator
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly KbkReader kbkReader;
        private readonly SyntheticAccountReader syntheticAccountReader;
        private readonly ICashOrderDao cashOrderDao;
        private readonly IHistoricalLogsCommandWriter historicalLogsWriter;

        public CashOrderCreator(
            IExecutionInfoContextAccessor executionInfoContext,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            KbkReader kbkReader,
            SyntheticAccountReader syntheticAccountReader,
            ICashOrderDao cashOrderDao,
            IHistoricalLogsCommandWriter historicalLogsWriter)
        {
            this.executionInfoContext = executionInfoContext;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.kbkReader = kbkReader;
            this.syntheticAccountReader = syntheticAccountReader;
            this.cashOrderDao = cashOrderDao;
            this.historicalLogsWriter = historicalLogsWriter;
        }

        public async Task<CreateResponse> CreateAsync(CashOrderSaveRequest request)
        {
            var context = executionInfoContext.ExecutionInfoContext;

            var cashOrder = await GetCashOrderAsync(request, (int)context.FirmId);
            cashOrder.Id = await cashOrderDao.CreateAsync(cashOrder);

            await historicalLogsWriter.WriteAsync(LogOperationType.Create, cashOrder);

            return new CreateResponse
            {
                Id = cashOrder.Id,
                DocumentBaseId = cashOrder.DocumentBaseId
            };
        }

        private async Task<CashOrder> GetCashOrderAsync(CashOrderSaveRequest request, int firmId)
        {
            var kbks = await kbkReader.GetByAccountCodeAsync((int)BudgetaryAccountCodes.UnifiedBudgetaryPayment);
            var kbk = kbks.FirstOrDefault();

            var cashOrder = request.CashOrder;
            cashOrder.Id = 0;
            cashOrder.FirmId = firmId;
            cashOrder.DocumentBaseId = request.DocumentBaseId;
            cashOrder.Date = cashOrder.Date.Date;
            cashOrder.CreateDate = DateTime.Now;
            cashOrder.ModifyDate = DateTime.Now;
            cashOrder.KbkId = kbk?.Id;
            cashOrder.AccountCode = (UnifiedBudgetaryAccountCodes?)kbk?.AccountCode;
            cashOrder.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(cashOrder.Date.Year);
            if (cashOrder.SalaryWorkerId == 0)
            {
                cashOrder.SalaryWorkerId = null;
            }
            cashOrder.SyntheticAccountTypeId = await syntheticAccountReader.GetIdByCodeAsync(cashOrder.GetSyntheticAccountCode());
            return cashOrder;
        }
    }
}
