using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using Moedelo.Money.PaymentOrders.DataAccess.WorkerPayments.DbModels;
using Moedelo.Money.PaymentOrders.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.WorkerPayments
{
    [InjectAsSingleton(typeof(IWorkerPaymentDao))]
    internal sealed class WorkerPaymentDao : MoedeloSqlDbExecutorBase, IWorkerPaymentDao
    {
        private readonly ISqlScriptReader scriptReader;

        public WorkerPaymentDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public async Task<IReadOnlyList<WorkerPayment>> GetByBaseIdAsync(int firmId, long documentBaseId)
        {
            var sqlTemplate = scriptReader.Get(this, "WorkerPayments.Scripts.Select.sql");
            var sql = new SqlQueryBuilder(sqlTemplate)
                .IncludeLine("DocumentBaseIdFilter")
                .ToString();
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            var response = await QueryAsync<WorkerPaymentDbModel>(queryObject);
            return response.Select(MapToModel).ToArray();
        }

        public async Task<IReadOnlyDictionary<long, WorkerPayment[]>> GetByBaseIdsAsync(
            int firmId, IReadOnlyCollection<long> documentBaseIds)
        {
            var sqlTemplate = scriptReader.Get(this, "WorkerPayments.Scripts.Select.sql");
            var sql = new SqlQueryBuilder(sqlTemplate)
                .IncludeLine("DocumentBaseIdsFilter")
                .ToString();
            var param = new { firmId, documentBaseIds };
            var queryObject = new QueryObject(sql, param);
            var response = await QueryAsync<WorkerPaymentDbModel>(queryObject);
            return response
                .GroupBy(x => x.DocumentBaseId)
                .ToDictionary(x => x.Key, x => x.Select(MapToModel).ToArray());
        }

        public Task InsertAsync(int firmId, long documentBaseId, IReadOnlyCollection<WorkerPayment> payments)
        {
            var param = new { firmId, documentBaseId };
            var tempTable = payments.Select(x => MapToDbModel(firmId, documentBaseId, x)).ToTemporaryTable("");
            var queryObject = new BulkCopyQueryObject("dbo.WorkerPayment", tempTable.DataTable);
            return BulkCopyAsync(queryObject);
        }

        public async Task OverwriteAsync(int firmId, long documentBaseId, IReadOnlyCollection<WorkerPayment> payments)
        {
            await DeleteAsync(firmId, documentBaseId);
            await InsertAsync(firmId, documentBaseId, payments);
        }

        public Task DeleteAsync(int firmId, long documentBaseId)
        {
            var sql = scriptReader.Get(this, "WorkerPayments.Scripts.Delete.sql");
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            return ExecuteAsync(queryObject);
        }

        private static WorkerPayment MapToModel(WorkerPaymentDbModel dbModel)
        {
            return new WorkerPayment
            {
                Id = dbModel.Id,
                WorkerId = dbModel.WorkerId,
                Sum = dbModel.PaymentSum,
                TaxationSystem = (TaxationSystemType)dbModel.TakeInTaxationSystem
            };
        }

        private static WorkerPaymentDbModel MapToDbModel(int firmId, long documentBaseId, WorkerPayment model)
        {
            return new WorkerPaymentDbModel
            {
                Id = model.Id,
                FirmId = firmId,
                DocumentBaseId = documentBaseId,
                WorkerId = model.WorkerId,
                PaymentSum = model.Sum,
                TakeInTaxationSystem = (short)model.TaxationSystem
            };
        }
    }
}
