using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Reports.DataAccess.Abstractions.Reconciliation;
using Moedelo.Money.Reports.DataAccess.Extensions;
using Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport;

namespace Moedelo.Money.Reports.DataAccess.Reconciliation
{
    [InjectAsSingleton(typeof(IReconciliationDao))]
    internal class ReconciliationDao : MoedeloSqlDbExecutorBase, IReconciliationDao
    {
        private readonly ISqlScriptReader scriptReader;

        public ReconciliationDao(
            ISettingRepository settings,
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<IReadOnlyList<SettlementAccountReconciliation>> GetSettlementAccountReconciliationAsync(IReadOnlyCollection<int> firmIds)
        {
            var sqlTemplate = scriptReader.Get(this, "Reconciliation.Scripts.GetSettlementAccountReconciliation.sql");
            var tempTable = firmIds.Select(x => new { Id = x }).ToTemporaryTable("firmIds");
            var queryObject = new QueryObject(sqlTemplate, null, tempTable);
            return QueryAsync<SettlementAccountReconciliation>(queryObject);
        }
    }
}
