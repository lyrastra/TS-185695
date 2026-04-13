using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using Moedelo.Money.Registry.DataAccess.Abstractions;
using Moedelo.Money.Registry.Domain;
using Moedelo.Money.Registry.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Registry.Domain.Models.SelfCostPayments;

namespace Moedelo.Money.Registry.DataAccess
{
    [InjectAsSingleton(typeof(IRegistryDao))]
    internal class RegistryDao : MoedeloSqlDbExecutorBase, IRegistryDao
    {
        private readonly ISqlScriptReader scriptReader;

        public RegistryDao(
            ISettingRepository settings,
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetReportsReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public async Task<ListWithCount<MoneyOperation>> GetAsync(int firmId, RegistryQuery query)
        {
            var sqlTemplate = scriptReader.Get(this, "Scripts.Get.sql");
            var queryObject = RegistryDaoQueryBuilder.Get(sqlTemplate, firmId, query);
            var querySettings = new QuerySetting(timeoutSeconds: 60);
            var items = await QueryAsync<OperationDbModel>(queryObject, querySettings).ConfigureAwait(false);

            return new ListWithCount<MoneyOperation>
            {
                Items = items.Select(RegistryDaoMapper.MapFromDbModel).ToArray(),
                TotalCount = items.FirstOrDefault()?.TotalCount ?? 0
            };
        }

        public async Task<List<MoneyOperation>> GetOutgoingPaymentsForTaxWidgetsAsync(int firmId, DateTime startDate, DateTime endDate)
        {
            var sqlTemplate = scriptReader.Get(this, "Scripts.GetOutgoingPaymentsForTaxWidgets.sql");
            var queryObject = RegistryDaoQueryBuilder.GetOutgoingPaymentsForTaxWidgets(sqlTemplate, firmId, startDate, endDate);
            var items = await QueryAsync<OperationDbModel>(queryObject).ConfigureAwait(false);

            return items.Select(RegistryDaoMapper.MapFromDbModel).ToList();
        }

        public Task<IReadOnlyList<SelfCostPayment>> GetBankSelfCostPaymentsAsync(int firmId,
            SelfCostPaymentRequest request)
        {
            var sqlTemplate = scriptReader.Get(this, "Scripts.GetBankSelfCostPayments.sql");
            var queryObject = RegistryDaoQueryBuilder.GetBankSelfCostPayments(sqlTemplate, firmId, request);
            return QueryAsync<SelfCostPayment>(queryObject);
        }

        public Task<IReadOnlyList<SelfCostPayment>> GetCashSelfCostPaymentsAsync(int firmId, SelfCostPaymentRequest request)
        {
            var sqlTemplate = scriptReader.Get(this, "Scripts.GetCashSelfCostPayments.sql");
            var queryObject = RegistryDaoQueryBuilder.GetCashSelfCostPayments(sqlTemplate, firmId, request);
            return QueryAsync<SelfCostPayment>(queryObject);
        }
    }
}
