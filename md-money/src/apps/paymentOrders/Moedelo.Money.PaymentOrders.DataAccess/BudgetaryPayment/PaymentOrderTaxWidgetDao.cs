using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.DataAccess.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IPaymentOrderTaxWidgetDao))]
    internal sealed class PaymentOrderTaxWidgetDao : MoedeloSqlDbExecutorBase, IPaymentOrderTaxWidgetDao
    {
        private readonly ISqlScriptReader scriptReader;

        public PaymentOrderTaxWidgetDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<IReadOnlyList<OrderTaxWidget>> GetBudgetaryTaxPaymentsAsync(int firmId, BudgetaryPaymentTaxWidgetRequest request)
        {
            var sql = new SqlQueryBuilder(
                    scriptReader.Get(this, "BudgetaryPayment.Scripts.GetBudgetaryTaxPayments.sql"))
                .IncludeLineIf("PeriodFilter", request.Quarter.HasValue)
                .ToString();

            var parameters = new
            {
                firmId,
                operationType = OperationType.BudgetaryPayment,
                BadStates = HiddenOperationStates.All,
                PaidStatus = PaymentStatus.Payed,
                request.BudgetaryAccountCode,
                request.Year,
                PeriodType = BudgetaryPeriodType.Quarter,
                PeriodNumber = request.Quarter,
                request.KbkId,
                UnifiedOperationType = OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment,
                UnifiedBudgetaryAccountCode = BudgetaryAccountCodes.UnifiedBudgetaryPayment
            };
            var queryObject = new QueryObject(sql, parameters);

            return QueryAsync<OrderTaxWidget>(queryObject);
        }
    }
}
