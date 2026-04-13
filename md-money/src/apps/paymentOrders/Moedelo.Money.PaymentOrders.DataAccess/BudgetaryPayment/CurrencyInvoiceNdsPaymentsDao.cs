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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.BudgetaryPayment
{
    [InjectAsSingleton(typeof(ICurrencyInvoiceNdsPaymentsDao))]
    internal sealed class CurrencyInvoiceNdsPaymentsDao : MoedeloSqlDbExecutorBase, ICurrencyInvoiceNdsPaymentsDao
    {
        private readonly ISqlScriptReader scriptReader;

        public CurrencyInvoiceNdsPaymentsDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor sqlDbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(sqlDbExecutor, settings.GetReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<IReadOnlyList<CurrencyInvoiceNdsPayment>> GetByCriteriaAsync(
            int firmId,
            CurrencyInvoiceNdsPaymentsRequest request)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "BudgetaryPayment.Scripts.GetCurrencyInvoiceNdsPayments.sql"))
                .IncludeLineIf("QueryByNumberFilter", !string.IsNullOrEmpty(request.QueryByNumber))
                .IncludeLineIf("StartDateFilter", request.StartDate.HasValue)
                .IncludeLineIf("EndDateFilter", request.EndDate.HasValue)
                .ToString();
            
            var parameters = new
            {
                firmId,
                request.Offset,
                request.Limit,
                operationTypes = new[] { OperationType.BudgetaryPayment, OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment },
                request.KbkIds,
                QueryByNumber = $"{request.QueryByNumber}",
                request.StartDate,
                request.EndDate,
                BadStates = HiddenOperationStates.All,
                BudgetaryAccountCodes = new[] { BudgetaryAccountCodes.Nds, BudgetaryAccountCodes.UnifiedBudgetaryPayment },
                PaidStatus = PaymentStatus.Payed
            };
            
            var queryObject = new QueryObject(sql, parameters);
            return QueryAsync<CurrencyInvoiceNdsPayment>(queryObject);
        }
    }
}