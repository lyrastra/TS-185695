using System.Threading.Tasks;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.PurseOperations.DataAccess.Extensions;
using Moedelo.Money.PurseOperations.Domain.Models;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Money.PurseOperations.DataAccess.Abstractions;

namespace Moedelo.Money.PurseOperations.DataAccess.PurseOperations
{
    [InjectAsSingleton(typeof(IPurseOperationDao))]
    internal class PurseOperationDao : MoedeloSqlDbExecutorBase, IPurseOperationDao
    {
        private readonly ISqlScriptReader scriptReader;

        public PurseOperationDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<PurseOperation> GetAsync(int firmId, long documentBaseId)
        {
            var sql = scriptReader.Get(this, "PurseOperations.Scripts.Select.sql");
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<PurseOperation>(queryObject);
        }
    }
}
