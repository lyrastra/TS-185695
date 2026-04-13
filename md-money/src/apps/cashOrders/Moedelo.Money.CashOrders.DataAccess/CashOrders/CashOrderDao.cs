using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.CashOrders.DataAccess.Abstractions;
using Moedelo.Money.CashOrders.DataAccess.Extensions;
using Moedelo.Money.CashOrders.Domain.Models;
using Moedelo.Money.Common.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.DataAccess.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderDao))]
    internal class CashOrderDao : MoedeloSqlDbExecutorBase, ICashOrderDao
    {
        private readonly ISqlScriptReader scriptReader;

        public CashOrderDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<CashOrder> GetAsync(int firmId, long documentBaseId)
        {
            var sql = scriptReader.Get(this, "CashOrders.Scripts.Select.sql");
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<CashOrder>(queryObject);
        }

        public Task<long> CreateAsync(CashOrder operation)
        {
            var sql = scriptReader.Get(this, "CashOrders.Scripts.Insert.sql");
            var queryObject = new QueryObject(sql, operation);
            return FirstOrDefaultAsync<long>(queryObject);
        }

        public Task UpdateAsync(CashOrder operation)
        {
            var sql = scriptReader.Get(this, "CashOrders.Scripts.Update.sql");
            var queryObject = new QueryObject(sql, operation);
            return ExecuteAsync(queryObject);
        }

        public Task DeleteAsync(int firmId, long documentBaseId)
        {
            var sql = scriptReader.Get(this, "CashOrders.Scripts.Delete.sql");
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            return ExecuteAsync(queryObject);
        }

        public Task<IReadOnlyList<OperationTypeResponse>> GetOperationTypeByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds)
        {
            var sql = scriptReader.Get(this, "CashOrders.Scripts.GetOperationTypeByBaseIds.sql");
            var tempTable = documentBaseIds.ToTempBigIntIds("BaseIds");
            var queryObject = new QueryObject(sql, new { firmId }, tempTable);
            return QueryAsync<OperationTypeResponse>(queryObject);
        }
    }
}
