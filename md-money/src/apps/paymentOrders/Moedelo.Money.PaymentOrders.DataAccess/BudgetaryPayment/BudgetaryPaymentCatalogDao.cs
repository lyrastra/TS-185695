using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentCatalogDao))]
    internal sealed class BudgetaryPaymentCatalogDao : MoedeloSqlDbExecutorBase, IBudgetaryPaymentCatalogDao
    {
        private readonly ISqlScriptReader scriptReader;

        public BudgetaryPaymentCatalogDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor sqlDbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(sqlDbExecutor, settings.GetReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public async Task<BudgetaryPaymentReason[]> GetPaymentReasonsAsync()
        {
            var sql = scriptReader.Get(this, "BudgetaryPayment.Scripts.GetActivePaymentReason.sql");
            var queryObject = new QueryObject(sql);
            var response = await QueryAsync<BudgetaryPaymentReason>(queryObject);
            return response.ToArray();
        }
    }
}
