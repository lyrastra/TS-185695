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
using Moedelo.Money.PaymentOrders.DataAccess.Outsource.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.Outsource
{
    [InjectAsSingleton(typeof(IOutsourceApproveDao))]
    internal class OutsourceApproveDao : MoedeloSqlDbExecutorBase, IOutsourceApproveDao
    {
        private readonly ISqlScriptReader scriptReader;

        public OutsourceApproveDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor sqlDbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(sqlDbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<bool> GetIsApprovedAsync(int firmId, long documentBaseId, DateTime initialDate)
        {
            var sql = scriptReader.Get(this, "Outsource.Scripts.GetIsApprovedById.sql");
            var queryParams = new
            {
                firmId,
                documentBaseId,
                initialDate,
                OutsourceApprovedState = OperationState.OutsourceApproved
            };
            var queryObject = new QueryObject(sql, queryParams);
            return FirstOrDefaultAsync<bool>(queryObject);
        }

        public Task<IReadOnlyList<IsApprovedResponse>> GetIsApprovedAsync(
            int firmId, IReadOnlyCollection<long> documentBaseIds, DateTime initialDate)
        {
            var sql = scriptReader.Get(this, "Outsource.Scripts.GetIsApprovedByIds.sql");
            var queryParams = new
            {
                firmId,
                initialDate,
                OutsourceApprovedState = OperationState.OutsourceApproved
            };
            var queryObject = new QueryObject(sql, queryParams, documentBaseIds.ToTempBigIntIds("temp_ids"));
            return QueryAsync<IsApprovedResponse>(queryObject);
        }

        public async Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationsApprovedAsync(
            AllOperationsApprovedRequest request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (request?.FirmIds is not { Count: > 0 })
            {
                return new Dictionary<int, bool>();
            }

            var sql = new SqlQueryBuilder(scriptReader.Get(this, "Outsource.Scripts.GetIsAllOperationApprovedByFirmIds.sql"))
                .IncludeLineIf("PaidStatusFilter", request.IsOnlyPaid)
                .ToString();

            var queryParams = new
            {
                startDate = request.StartDate,
                endDate = request.EndDate,
                initialDate = request.InitialDate,
                OutsourceApprovedState = OperationState.OutsourceApproved,
                PaidStatus = PaymentStatus.Payed
            };

            var queryObject = new QueryObject(sql, queryParams, request.FirmIds.ToTempIntIds("firmIds"));
            var dbModels = await QueryAsync<OperationsApprovedDbModel>(queryObject, cancellationToken: ct);

            return dbModels.ToDictionary(x => x.FirmId, x => x.AllApproved);
        }

        public Task TrySetIsApprovedAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var sql = scriptReader.Get(this, "Outsource.Scripts.TrySetIsApproved.sql");
            var queryParam = new
            {
                OutsourceApprovedState = OperationState.OutsourceApproved,
                OperationStateDefault = OperationState.Default,
                OperationStateImported = OperationState.Imported
            };
            var queryObject = new QueryObject(sql, queryParam, documentBaseIds.ToTempBigIntIds("temp_ids"));
            return ExecuteAsync(queryObject);
        }

        public Task TryUnsetIsApprovedAsync(int firmId, long documentBaseId, DateTime initialDate)
        {
            var sql = scriptReader.Get(this, "Outsource.Scripts.TryUnsetIsApproved.sql");
            var queryParams = new
            {
                firmId,
                documentBaseId,
                initialDate,
                OutsourceApprovedState = OperationState.OutsourceApproved,
                OperationStateDefault = OperationState.Default
            };
            var queryObject = new QueryObject(sql, queryParams);
            return ExecuteAsync(queryObject);
        }
    }
}
