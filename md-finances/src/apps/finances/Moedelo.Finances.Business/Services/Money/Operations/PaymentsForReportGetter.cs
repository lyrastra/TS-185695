using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.CashOrders;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class PaymentsForReportGetter : IPaymentsForReportGetter
    {
        private readonly IPaymentOrderOperationDao paymentOrderOperationDao;
        private readonly ICashOrderOperationDao cashOrderOperationDao;

        public PaymentsForReportGetter(
            IPaymentOrderOperationDao paymentOrderOperationDao,
            ICashOrderOperationDao cashOrderOperationDao)
        {
            this.paymentOrderOperationDao = paymentOrderOperationDao;
            this.cashOrderOperationDao = cashOrderOperationDao;
        }

        public Task<List<PaymentOrderOperation>> GetBudgetaryPaymentsAsync(IUserContext userContext, BudgetaryPaymentOrderOperationQueryParams queryParams)
        {
            return paymentOrderOperationDao.GetBudgetaryPaymentsAsync(userContext.FirmId, queryParams);
        }

        public async Task<List<PaymentOrderOperation>> GetBudgetaryPaymentsWithSubPaymentsAsync(IUserContext userContext, BudgetaryPaymentOrderOperationQueryParams queryParams)
        {
            var budgetaryPayments = await paymentOrderOperationDao
                .GetBudgetaryPaymentsWithUnifiedTaxPaymentsAsync(userContext.FirmId, queryParams)
                .ConfigureAwait(false);

            foreach (var budgetaryPayment in budgetaryPayments)
            {
                budgetaryPayment.BudgetaryPeriodDate = GetPeriodDate(
                    budgetaryPayment.BudgetaryPeriodNumber,
                    budgetaryPayment.BudgetaryPeriodType, 
                    budgetaryPayment.BudgetaryPeriodYear);
            }

            return budgetaryPayments;
        }

        public async Task<List<CashOrderOperation>> GetBudgetaryCashPaymentsAsync(IUserContext userContext, BudgetaryCashOrderOperationQueryParams queryParams)
        {
            return await cashOrderOperationDao.GetBudgetaryPaymentsAsync(userContext.FirmId, queryParams).ConfigureAwait(false);
        }

        public async Task<List<CashOrderOperation>> GetBudgetaryCashPaymentsWithSubPaymentsAsync(IUserContext userContext, BudgetaryCashOrderOperationQueryParams queryParams)
        {
            var budgetaryPayments = await cashOrderOperationDao
                .GetBudgetaryPaymentsWithUnifiedTaxPaymentsAsync(userContext.FirmId, queryParams)
                .ConfigureAwait(false);

            foreach (var budgetaryPayment in budgetaryPayments)
            {
                budgetaryPayment.BudgetaryPeriodDate = GetPeriodDate(
                    budgetaryPayment.BudgetaryPeriodNumber,
                    budgetaryPayment.BudgetaryPeriodType,
                    budgetaryPayment.BudgetaryPeriodYear);
            }

            return budgetaryPayments;
        }

        private static DateTime GetPeriodDate(int? budgetaryPeriodNumber, BudgetaryPeriodType? budgetaryPeriodType, int? budgetaryPeriodYear)
        {
            var period = new BudgetaryPeriod(
                budgetaryPeriodNumber ?? 0,
                budgetaryPeriodType ?? BudgetaryPeriodType.None,
                budgetaryPeriodYear ?? 0);
            return period.LastDayInPeriod;
        }
    }
}
