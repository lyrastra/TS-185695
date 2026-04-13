using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments;
using Moedelo.Money.Common.DataAccess.Extensions;
using Moedelo.Money.Common.DataAccess.UnifiedTaxPayments.DbModels;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Common.DataAccess.UnifiedTaxPayments
{
    [InjectAsSingleton(typeof(IUnifiedTaxPaymentDao))]
    internal class UnifiedTaxPaymentDao : MoedeloSqlDbExecutorBase, IUnifiedTaxPaymentDao
    {
        private readonly ISqlScriptReader scriptReader;

        public UnifiedTaxPaymentDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<UnifiedBudgetarySubPayment> GetByBaseIdAsync(int firmId, long documentBaseId)
        {
            var sqlTemplate = scriptReader.Get(this, "UnifiedTaxPayments.Scripts.Select.sql");
            var sql = new SqlQueryBuilder(sqlTemplate)
                .IncludeLine("DocumentBaseIdFilter")
                .ToString();
            var param = new { firmId, documentBaseId };
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<UnifiedBudgetarySubPayment>(queryObject);
        }

        private static readonly IReadOnlyList<UnifiedBudgetarySubPayment> emptyList = [];

        public Task<IReadOnlyList<UnifiedBudgetarySubPayment>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds, CancellationToken ct)
        {
            if (documentBaseIds is { Count: 0})
            {
                return Task.FromResult(emptyList);
            }

            var sqlTemplate = scriptReader.Get(this, "UnifiedTaxPayments.Scripts.Select.sql");
            var sql = new SqlQueryBuilder(sqlTemplate)
                .IncludeLine("DocumentBaseIdsFilter")
                .ToString();
            var param = new { firmId };

            var tempTableList = new List<TemporaryTable>
            {
                documentBaseIds.ToTempBigIntIds("BaseIds")
            };

            var queryObject = new QueryObject(sql, param, temporaryTables: tempTableList);
            return QueryAsync<UnifiedBudgetarySubPayment>(queryObject, cancellationToken:ct);
        }

        public Task<IReadOnlyList<UnifiedBudgetarySubPayment>> GetByParentBaseIdAsync(int firmId, long parentBaseId)
        {
            var sqlTemplate = scriptReader.Get(this, "UnifiedTaxPayments.Scripts.Select.sql");
            var sql = new SqlQueryBuilder(sqlTemplate)
                .IncludeLine("ParentBaseIdFilter")
                .ToString();
            var param = new { firmId, parentBaseId };

            var queryObject = new QueryObject(sql, param);
            return QueryAsync<UnifiedBudgetarySubPayment>(queryObject);
        }

        public Task<IReadOnlyList<UnifiedBudgetarySubPayment>> GetByParentBaseIdsAsync(
            int firmId, IReadOnlyCollection<long> parentsBaseIds)
        {
            var sqlTemplate = scriptReader.Get(this, "UnifiedTaxPayments.Scripts.Select.sql");
            var sql = new SqlQueryBuilder(sqlTemplate)
                .IncludeLine("ParentBaseIdsFilter")
                .ToString();
            var param = new { firmId };

            var tempTableList = new List<TemporaryTable>
            {
                parentsBaseIds.ToTempBigIntIds("BaseIds")
            };

            var queryObject = new QueryObject(sql, param, temporaryTables: tempTableList);
            return QueryAsync<UnifiedBudgetarySubPayment>(queryObject);
        }

        public Task InsertAsync(int firmId, long documentBaseId, IReadOnlyCollection<UnifiedBudgetarySubPayment> payments)
        {
            if (payments == null || payments.Count == 0)
            {
                return Task.CompletedTask;
            }

            var tempTable = payments.Select(x => MapToDbModel(firmId, documentBaseId, x)).ToTemporaryTable("");
            var queryObject = new BulkCopyQueryObject("dbo.UnifiedTaxPayment", tempTable.DataTable);
            return BulkCopyAsync(queryObject);
        }

        public async Task<IReadOnlyList<long>> OverwriteAsync(int firmId, long parentBaseId, IReadOnlyCollection<UnifiedBudgetarySubPayment> payments)
        {
            IReadOnlyList<long> ids = new List<long>();
            var retries = 0;
            var success = false;
            var random = new Random();
            var randomNumber = random.Next(50, 101);
            // AD-2209: Начали сыпаться дедлоки. Быстрое решение, попробовать еще раз, в надежде, что дедлок пройдет.
            do
            {
                try
                {
                    ids = await SimpleOverwriteAsync(firmId, parentBaseId, payments);
                    success = true;
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 1205 && retries <= 20)
                    {
                        await Task.Delay(randomNumber);
                        ++retries;
                    }
                    else
                        throw;
                }
            } while (!success);

            return ids;
        }

        private async Task<IReadOnlyList<long>> SimpleOverwriteAsync(int firmId, long parentBaseId, IReadOnlyCollection<UnifiedBudgetarySubPayment> payments)
        {
            var sql = scriptReader.Get(this, "UnifiedTaxPayments.Scripts.Overwrite.sql");
            var param = new { firmId, parentBaseId };
            var tempTable = payments.ToTemporaryTable("Payments");
            var queryObject = new QueryObject(sql, param, tempTable);
            var result = await QueryAsync<long?>(queryObject);
            return result.Where(x => x.HasValue).Select(x => x.Value).ToArray();
        }

        public Task<IReadOnlyList<long>> DeleteAsync(int firmId, long parentBaseId)
        {
            var sql = scriptReader.Get(this, "UnifiedTaxPayments.Scripts.Delete.sql");
            var param = new { firmId, parentBaseId };
            var queryObject = new QueryObject(sql, param);
            return QueryAsync<long>(queryObject);
        }

        public Task UpdateTaxPostingTypeAsync(int firmId, long documentBaseId, ProvidePostingType taxPostingType)
        {
            var sql = scriptReader.Get(this, "UnifiedTaxPayments.Scripts.SetTaxPostingType.sql");
            var param = new { firmId, documentBaseId, taxPostingType };
            var queryObject = new QueryObject(sql, param);
            return ExecuteAsync(queryObject);
        }

        private static UnifiedTaxPaymentDbModel MapToDbModel(int firmId, long parentBaseId, UnifiedBudgetarySubPayment subPayment)
        {
            return new UnifiedTaxPaymentDbModel
            {
                FirmId = firmId,
                ParentDocumentId = parentBaseId,
                DocumentBaseId = subPayment.DocumentBaseId,
                KbkNumberId = subPayment.KbkNumberId,
                PeriodType = subPayment.PeriodType,
                PeriodNumber = subPayment.PeriodNumber,
                PeriodYear = subPayment.PeriodYear,
                PaymentSum = subPayment.PaymentSum,
                PatentId = subPayment.PatentId,
                TradingObjectId = subPayment.TradingObjectId,
                TaxPostingType = subPayment.TaxPostingType
            };
        }
    }
}
