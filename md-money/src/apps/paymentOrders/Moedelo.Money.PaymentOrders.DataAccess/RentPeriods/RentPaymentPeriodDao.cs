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
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.RentPeriods
{
    [InjectAsSingleton(typeof(IRentPaymentPeriodDao))]
    internal class RentPaymentPeriodDao : MoedeloSqlDbExecutorBase, IRentPaymentPeriodDao
    {
        private readonly ISqlScriptReader scriptReader;

        public RentPaymentPeriodDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor sqlDbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer): base(sqlDbExecutor, settings.GetReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<IReadOnlyList<RentPaymentPeriod>> GetAsync(int firmId, IReadOnlyCollection<long> ids)
        {
            var sql = scriptReader.Get(this, "RentPeriods.Scripts.GetByPaymentIds.sql");
            var param = new { firmId };
            var tempTable = ids.Select(x => new { DocumentBaseId = x }).ToTemporaryTable("Payments");
            var queryObject = new QueryObject(sql, param, tempTable);
            return QueryAsync<RentPaymentPeriod>(queryObject);
        }
    }
}
