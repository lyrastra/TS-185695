using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentDao))]
    internal sealed class BudgetaryPaymentDao : MoedeloSqlDbExecutorBase, IBudgetaryPaymentDao
    {
        private readonly ISqlScriptReader scriptReader;

        public BudgetaryPaymentDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor sqlDbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(sqlDbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<string> GetPayerKppAsync(int firmId, long documentBaseId)
        {
            var param = new { firmId, documentBaseId };
            var sql = scriptReader.Get(this, "BudgetaryPayment.Scripts.GetPayerKpp.sql");
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<string>(queryObject);
        }

        public Task SetPayerKppAsync(int firmId, long documentBaseId, string kpp)
        {
            var param = new
            {
                firmId,
                documentBaseId,
                kpp
            };
            var sql = scriptReader.Get(this, "BudgetaryPayment.Scripts.SetPayerKpp.sql");
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<string>(queryObject);
        }
    }
}
