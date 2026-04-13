using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PurseOperations.DataAccess.Abstractions;
using Moedelo.Money.PurseOperations.DataAccess.Extensions;

namespace Moedelo.Money.PurseOperations.DataAccess.PurseOperations
{
    [InjectAsSingleton(typeof(IPurseOperationReadOnlyDao))]
    public class PurseOperationReadOnlyDao: MoedeloSqlDbExecutorBase, IPurseOperationReadOnlyDao
    {
        private readonly ISqlScriptReader scriptReader;

        public PurseOperationReadOnlyDao(
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
            var sql = scriptReader.Get(this, "PurseOperations.Scripts.GetDocumentsStatusByBaseIds.sql");
            var tempTable = documentBaseIds.ToTempBigIntIds("BaseIds");
            var queryObject = new QueryObject(sql, null, tempTable);
            return QueryAsync<DocumentStatus>(queryObject);
        }
    }
}