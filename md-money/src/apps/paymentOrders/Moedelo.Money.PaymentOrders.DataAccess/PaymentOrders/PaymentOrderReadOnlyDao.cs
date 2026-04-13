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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderReadOnlyDao))]
    public class PaymentOrderReadOnlyDao : MoedeloSqlDbExecutorBase, IPaymentOrderReadOnlyDao
    {
        private readonly ISqlScriptReader scriptReader;

        public PaymentOrderReadOnlyDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<IReadOnlyList<int>> GetOutgoingNumbersAsync(int firmId, int settlementAccountId, int? year, int? cut)
        {

            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.GetOutgoingNumbers.sql"))
                .ToString();

            var param = new
            {
                firmId,
                settlementAccountId,
                startOfYear = year.HasValue ? new DateTime(year.Value, 1, 1) : new DateTime(DateTime.Now.Year, 1, 1),
                endOfYear = year.HasValue ? new DateTime(year.Value, 12, 31) : new DateTime(DateTime.Now.Year, 12, 31),
                cut = cut ?? 0,
                badStates = HiddenOperationStates.All,
                operationStateDefault = OperationState.Default,
            };
            var queryObject = new QueryObject(sql, param);
            return QueryAsync<int>(queryObject);
        }

        public Task<bool> GetIsPaidAsync(int firmId, long documentBaseId)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.GetIsPaid.sql"))
                .ToString();

            var param = new
            {
                firmId,
                documentBaseId,
                truePaidStatus = (int)PaymentStatus.Payed
            };
            var queryObject = new QueryObject(sql, param);

            return FirstOrDefaultAsync<bool>(queryObject);
        }
        
        public Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusAsync(DocumentsStatusRequest request)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.GetDocumentsStatus.sql"))
                .IncludeLineIf("PassedOutsourcingCheckFilter", request.IsPassedOutsourcingCheck.HasValue)
                .IncludeLineIf("PaidStatusFilter", request.IsAllPaid.HasValue)
                .ToString();

            var badOutsourceState = new List<OutsourceState> { OutsourceState.ConfirmInvalid };

            var param = new
            {
                paidStatus = PaymentStatus.Payed,
                badStates = HiddenOperationStates.All,
                operationStateDefault = OperationState.Default,
                badOutsourceState
            };
            var tempTable = request.DocBaseIds.ToTempBigIntIds("BaseIds");
            
            var queryObject = new QueryObject(sql, param, tempTable);
            return QueryAsync<DocumentStatus>(queryObject);
        }
    }
}
