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
using Moedelo.Money.PaymentOrders.DataAccess.RentPeriods.DbModels;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.RentPeriods
{
    [InjectAsSingleton(typeof(IRentPeriodDao))]
    internal sealed class RentPeriodDao : MoedeloSqlDbExecutorBase, IRentPeriodDao
    {
        private readonly ISqlScriptReader scriptReader;

        public RentPeriodDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor sqlDbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(sqlDbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task DeleteAsync(int firmId, long documentBaseId)
        {
            var sql = scriptReader.Get(this, "RentPeriods.Scripts.Delete.sql");
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            return ExecuteAsync(queryObject);
        }

        public Task<IReadOnlyList<RentPeriod>> GetAsync(int firmId, long documentBaseId)
        {
            var sql = scriptReader.Get(this, "RentPeriods.Scripts.Select.sql");
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            return QueryAsync<RentPeriod>(queryObject);
        }

        public Task InsertAsync(int firmId, long documentBaseId, IReadOnlyCollection<RentPeriod> periods)
        {
            var tempTable = periods.Select(x => MapToDbModel(firmId, documentBaseId, x)).ToTemporaryTable("");
            var queryObject = new BulkCopyQueryObject("dbo.RentalPayment", tempTable.DataTable);
            return BulkCopyAsync(queryObject);
        }

        public Task OverwriteAsync(int firmId, long documentBaseId, IReadOnlyCollection<RentPeriod> periods)
        {
            var sql = scriptReader.Get(this, "RentPeriods.Scripts.Overwrite.sql");
            var param = new { firmId, documentBaseId };
            var tempTable = periods.ToTemporaryTable("Periods");
            var queryObject = new QueryObject(sql, param, tempTable);
            return ExecuteAsync(queryObject);
        }

        private static RentalPaymentDbModel MapToDbModel(int firmId, long documentBaseId, RentPeriod period)
        {
            return new RentalPaymentDbModel
            {
                Id = period.Id,
                FirmId = firmId,
                PaymentBaseId = documentBaseId,
                RentalPaymentItemId = period.RentalPaymentItemId,
                PaymentSum = period.PaymentSum    
            };
        }
    }
}
