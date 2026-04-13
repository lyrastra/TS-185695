using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using Moedelo.Money.PaymentOrders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderDao))]
    internal sealed class PaymentOrderDao : MoedeloSqlDbExecutorBase, IPaymentOrderDao
    {
        private readonly ISqlScriptReader scriptReader;

        public PaymentOrderDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<PaymentOrder> GetAsync(int firmId, long documentBaseId)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.Select.sql"))
                .IncludeLine("SelectOneFilter")
                .ToString();

            var param = new
            {
                firmId,
                documentBaseId,
                OperationStateDefault = OperationState.Default,
                badStates = HiddenOperationStates.All
            };
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<PaymentOrder>(queryObject);
        }

        public Task<IReadOnlyList<PaymentOrder>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds, OperationType operationType)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.Select.sql"))
                .IncludeLine("SelectListFilter")
                .IncludeLine("OperationTypeFilter")
                .ToString();

            var param = new
            {
                firmId,
                operationType,
                OperationStateDefault = OperationState.Default,
                badStates = HiddenOperationStates.All
            };
            var tempTable = documentBaseIds.ToTempBigIntIds("BaseIds");
            var queryObject = new QueryObject(sql, param, tempTable);
            return QueryAsync<PaymentOrder>(queryObject);
        }

        public Task<IReadOnlyList<long>> GetBaseIdsByOperationTypeAsync(int firmId, OperationType operationType, int? year)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.GetBaseIdByType.sql")).ToString();

            if ((year ?? 0) == 0)
                year = DateTime.Now.Year;

            var param = new
            {
                firmId,
                operationType,
                startDate = new DateTime(year.Value, 1, 1),
                endDate = new DateTime(year.Value, 12, 31),
                operationStateDefault = OperationState.Default,
                badStates = HiddenOperationStates.All
            };
            var queryObject = new QueryObject(sql, param);
            return QueryAsync<long>(queryObject);
        }

        public Task<bool> GetIsFromImportAsync(int firmId, long documentBaseId)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.GetIsFromImport.sql"))
                .IncludeLine("SelectOneFilter")
                .ToString();

            var param = new
            {
                firmId,
                documentBaseId
            };
            var queryObject = new QueryObject(sql, param);

            return FirstOrDefaultAsync<bool>(queryObject);
        }

        public Task<long> InsertAsync(PaymentOrder operation)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.Insert.sql");
            operation.NullifySparseColumns();
            operation.TruncateStringsColumns();
            var queryObject = new QueryObject(sql, operation);
            return FirstOrDefaultAsync<long>(queryObject);
        }

        public Task UpdateAsync(PaymentOrder operation)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.Update.sql");
            operation.NullifySparseColumns();
            operation.TruncateStringsColumns();
            var queryObject = new QueryObject(sql, operation);
            return ExecuteAsync(queryObject);
        }

        public Task DeleteAsync(int firmId, long documentBaseId)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.Delete.sql");
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            return ExecuteAsync(queryObject);
        }

        public Task<long?> GetBaseIdByIdAsync(int firmId, long id)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.GetBaseIdById.sql");
            var queryObject = new QueryObject(sql, new { firmId, id });
            return FirstOrDefaultAsync<long?>(queryObject);
        }

        public Task<IReadOnlyList<OperationTypeResponse>> GetOperationTypeByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.GetOperationTypeByBaseIds.sql");
            var tempTable = documentBaseIds.ToTempBigIntIds("BaseIds");
            var queryObject = new QueryObject(sql, new { firmId }, tempTable);
            return QueryAsync<OperationTypeResponse>(queryObject);
        }

        public Task<HashSet<long>> DeleteInvalidAsync(int firmId, IReadOnlyCollection<long> documentBaseIds)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.DeleteInvalid.sql");

            var param = new { firmId, OperationStateDefault = OperationState.Default };
            var tempTables = new[]
            {
                documentBaseIds.ToTempBigIntIds("BaseIds"),
                HiddenOperationStates.All.Cast<int>().ToTempIntIds("BadStates")
            };
            var queryObject = new QueryObject(sql, param, temporaryTables: tempTables);
            return HashSetQueryAsync<long>(queryObject);
        }

        public Task ApplyIgnoreNumberAsync(int firmId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || !documentBaseIds.Any())
            {
                return Task.CompletedTask;
            }

            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.ApplyIgnoreNumber.sql");

            var param = new { firmId };
            var tempTables = new[]
            {
                documentBaseIds.ToTempBigIntIds("BaseIds")
            };
            var queryObject = new QueryObject(sql, param, temporaryTables: tempTables);
            return ExecuteAsync(queryObject);
        }

        public Task<IReadOnlyList<PaymentOrder>> GetByOperationStateAsync(int firmId, OperationState operationState)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.Select.sql"))
                .IncludeLine("OperationStateFilter")
                .ToString();

            var param = new
            {
                firmId,
                operationState,
                badStates = HiddenOperationStates.All,
                OperationStateDefault = OperationState.Default,
            };
            var queryObject = new QueryObject(sql, param);
            return QueryAsync<PaymentOrder>(queryObject);
        }

        public Task<HashSet<long>> ApproveImportedAsync(int firmId, int? settlementAccountId, DateTime? skipline)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.ApproveImported.sql");

            var param = new
            {
                firmId,
                settlementAccountId,
                Skipline = skipline,
                OperationStateDefault = OperationState.Default,
                OperationStateImported = OperationState.Imported
            };
            var queryObject = new QueryObject(sql, param);
            return HashSetQueryAsync<long>(queryObject);
        }
    }
}
