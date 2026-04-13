using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money;
using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Tables
{
    [InjectAsSingleton(typeof(IMoneyTableDao))]
    internal sealed class MoneyTableDao(IMoedeloDbExecutor dbExecutor) : IMoneyTableDao
    {
        public async Task<UnrecognizedMoneyTableResponse> GetUnrecognizedAsync(int firmId, DateTime initialDate, MoneyTableRequest request, CancellationToken cancellationToken)
        {
            if (IsInvalidSource(request.SourceType))
            {
                return new UnrecognizedMoneyTableResponse();
            }
            var result = new UnrecognizedMoneyTableResponse();
            var queryObject = MoneyTableSqlBuilder.CreateGetUnrecognizedTableQueryObject(firmId, initialDate, request)
                .WithAuditTrailSpanName("MoneyTableDao.GetUnrecognized");
            await dbExecutor.QueryMultipleAsync(queryObject, async reader =>
            {
                var operations = await reader.ReadListAsync<UnrecognizedMoneyTableOperation>().ConfigureAwait(false);
                var summary = operations.First();
                result.TotalCount = summary.TotalCount;
                result.Operations = operations.Skip(1).ToList();
            }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result;
        }

        public Task<int> GetUnrecognizedOperationsCountAsync(int firmId, DateTime initialDate, MoneySourceType sourceType, long? sourceId, CancellationToken cancellationToken)
        {
            if (IsInvalidSource(sourceType))
            {
                return Task.FromResult(0);
            }
            var queryObject = MoneyTableSqlBuilder.CreateGetUnrecognizedOperationCountQueryObject(firmId, initialDate, sourceType, sourceId)
                .WithAuditTrailSpanName("MoneyTableDao.GetUnrecognizedOperationsCount");
            return dbExecutor.FirstOrDefaultAsync<int>(queryObject, cancellationToken: cancellationToken);
        }

        public async Task<ImportedMoneyTableResponse> GetImportedAsync(int firmId, DateTime initialDate, MoneyTableRequest request, CancellationToken cancellationToken)
        {
            if (IsInvalidSource(request.SourceType))
            {
                return new ImportedMoneyTableResponse();
            }
            var result = new ImportedMoneyTableResponse();
            var queryObject = MoneyTableSqlBuilder.CreateGetImportedTableQueryObject(firmId, initialDate, request)
                .WithAuditTrailSpanName("MoneyTableDaoGetImported");
            await dbExecutor.QueryMultipleAsync(queryObject, async reader =>
            {
                var operations = await reader.ReadListAsync<ImportedMoneyTableOperation>().ConfigureAwait(false);
                var summary = operations.First();
                result.TotalCount = summary.TotalCount;
                result.Operations = operations.Skip(1).ToList();
            }, cancellationToken: cancellationToken).ConfigureAwait(false);
            return result;
        }

        public Task<int> GetImportedCountAsync(int firmId, DateTime initialDate, MoneySourceType sourceType, long? sourceId)
        {
            if (IsInvalidSource(sourceType))
            {
                return Task.FromResult(0);
            }
            var queryObject = MoneyTableSqlBuilder.CreateGetImportedOperationsCountQueryObject(firmId, initialDate, sourceType, sourceId)
                .WithAuditTrailSpanName("MoneyTableDao.GetImportedCount");
            return dbExecutor.FirstOrDefaultAsync<int>(queryObject);
        }

        public async Task<OutsourceProcessingMoneyTableResponse> GetOutsourceProcessingAsync(int firmId,
            DateTime initialDate, OutsourceProcessingTableRequest request, CancellationToken ctx)
        {
            if (IsInvalidSource(request.SourceType))
            {
                return new OutsourceProcessingMoneyTableResponse();
            }
            var result = new OutsourceProcessingMoneyTableResponse();
            var queryObject = MoneyTableSqlBuilder.CreateGetOutsourceProcessingTableQueryObject(firmId, initialDate, request)
                .WithAuditTrailSpanName("MoneyTableDao.GetOutsourceProcessing");
            await dbExecutor.QueryMultipleAsync(queryObject, async reader =>
            {
                var operations = await reader.ReadListAsync<OutsourceProcessingMoneyTableOperation>().ConfigureAwait(false);
                var summary = operations.First();
                result.TotalCount = summary.TotalCount;
                result.Operations = operations.Skip(1).ToList();
            }, cancellationToken: ctx).ConfigureAwait(false);
            return result;
        }

        public async Task<int> GetOutsourceProcessingCountAsync(int firmId, DateTime initialDate, OutsourceProcessingTableRequest request)
        {
            if (IsInvalidSource(request.SourceType))
            {
                return 0;
            }

            var queryObject = MoneyTableSqlBuilder.CreateGetOutsourceProcessingTableQueryObject(
                firmId,
                initialDate,
                request,
                onlyTotalCount: true)
                .WithAuditTrailSpanName("MoneyTableDao.GetOutsourceProcessingCount");

            var totalCount = 0;
            await dbExecutor.QueryMultipleAsync(queryObject, async reader =>
            {
                var row = await reader
                    .ReadFirstOrDefaultAsync<OutsourceProcessingMoneyTableOperation>()
                    .ConfigureAwait(false);

                totalCount = row.TotalCount;
            }).ConfigureAwait(false);

            return totalCount;
        }

        public async Task<MainMoneyMultiCurrencyTableResponse> GetMultiCurrencyTableAsync(int firmId,
            DateTime initialDate, DateTime initialSummaryDate, MainMoneyTableRequest request, CancellationToken cancellationToken)
        {
            var response = new MainMoneyMultiCurrencyTableResponse
            {
                Operations = Array.Empty<MainMoneyTableOperation>(),
                Summaries = Array.Empty<MainMoneyMultiCurrencyTableSummary>()
            };

            var queryObject = MoneyTableSqlBuilder.CreateGetMainTableQueryObject(firmId, initialDate, initialSummaryDate, request)
                .WithAuditTrailSpanName("MoneyTableDao.GetMultiCurrencyTable");
            await dbExecutor.QueryMultipleAsync(queryObject, async reader =>
            {
                var operations = await reader.ReadListAsync<MainMoneyTableOperation>().ConfigureAwait(false);
                response.Operations = operations.Where(bo => !bo.IsSummary).ToArray();
                response.Summaries = operations.Where(x => x.IsSummary).Select(Map).GroupBy(summary => summary.Currency).ToDictionary(el => el.Key, el => el.ToList())
                  .Select(summaries => new MainMoneyMultiCurrencyTableSummary
                  {
                      Currency = summaries.Key,
                      StartBalance = summaries.Value.Sum(summary => summary.StartBalance),
                      EndBalance = summaries.Value.Sum(summary => summary.EndBalance),
                      IncomingBalance = summaries.Value.Sum(summary => summary.IncomingBalance),
                      OutgoingBalance = summaries.Value.Sum(summary => summary.OutgoingBalance),
                      IncomingCount = summaries.Value.Sum(summary => summary.IncomingCount),
                      IncomingDate = summaries.Value.Select(summary => summary.IncomingDate).FirstOrDefault(),
                      OutgoingCount = summaries.Value.Sum(summary => summary.OutgoingCount),
                      OutgoingDate = summaries.Value.Select(summary => summary.OutgoingDate).FirstOrDefault(),
                      HasOperations = summaries.Value.Select(summary => summary.HasOperations).Any(),
                      TotalCount = summaries.Value.Sum(summary => summary.TotalCount),
                      Operations = summaries.Value.Select(summary => summary.Operations).FirstOrDefault()
                  }).ToArray();
            }, settings: new QuerySetting(timeout: 120), cancellationToken: cancellationToken).ConfigureAwait(false);

            return response;
        }

        private static MainMoneyMultiCurrencyTableSummary Map(MainMoneyTableOperation summary)
        {
            return new MainMoneyMultiCurrencyTableSummary
            {
                StartBalance = summary.StartBalance,
                EndBalance = summary.EndBalance,
                IncomingCount = summary.IncomingCount,
                IncomingBalance = summary.IncomingBalance,
                IncomingDate = summary.IncomingDate,
                OutgoingCount = summary.OutgoingCount,
                OutgoingBalance = summary.OutgoingBalance,
                OutgoingDate = summary.OutgoingDate,
                TotalCount = summary.TotalCount,
                HasOperations = summary.HasOperations,
                Currency = GetCurrency(summary)
            };
        }

        private static Currency GetCurrency(MainMoneyTableOperation summary)
        {
            return summary.SettlementType == SettlementAccountType.Default ? Currency.RUB : summary.Currency;
        }

        private static bool IsInvalidSource(MoneySourceType sourceType)
        {
            return sourceType != MoneySourceType.All && sourceType != MoneySourceType.SettlementAccount;
        }
    }
}