using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.PostgreSqlDataAccess.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;
using Moedelo.Money.Numeration.DataAccess.Abstractions.PaymentOrders;
using System.Threading.Tasks;

namespace Moedelo.Money.Numeration.DataAccess.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentOrderNumberDao))]
    public class PaymentOrderNumberDao : MoedeloPostgreSqlDbExecutorBase, IPaymentOrderNumberDao
    {
        private readonly ISqlScriptReader scriptReader;

        public PaymentOrderNumberDao(
            ISqlScriptReader scriptReader,
            IPostgreSqlExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetPaymentOrderNumerationConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<int> GetLast(int firmId, int settlementAccountId, int year)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.GetLast.sql");
            var param = new
            {
                firmId,
                settlementAccountId,
                year
            };
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<int>(queryObject);
        }

        public Task SetLast(int firmId, int settlementAccountId, int year, int number)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.Save.sql");
            var param = new
            {
                firmId,
                settlementAccountId,
                year,
                number
            };
            var queryObject = new QueryObject(sql, param);
            return ExecuteAsync(queryObject);
        }
    }
}