using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.CashOrders.DataAccess.Abstractions;
using Moedelo.Money.CashOrders.DataAccess.Extensions;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.CashOrders.DataAccess.CashOrders
{
    [InjectAsSingleton(typeof(ICashOrderReadOnlyDao))]
    public class CashOrderReadOnlyDao: MoedeloSqlDbExecutorBase, ICashOrderReadOnlyDao
    {
        private readonly ISqlScriptReader scriptReader;

        public CashOrderReadOnlyDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }
        
        public Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var sql = scriptReader.Get(this, "CashOrders.Scripts.GetDocumentsStatusByBaseIds.sql");
            var tempTable = documentBaseIds.ToTempBigIntIds("BaseIds");
            var queryObject = new QueryObject(sql, null, tempTable);
            return QueryAsync<DocumentStatus>(queryObject);
        }
    }
}