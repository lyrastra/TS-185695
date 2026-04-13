using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.DataAccess.Money.Reconcilation.Mappers;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Reconcilation;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;
using Moedelo.PaymentImport.Client.Reconciliation;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.Finances.DataAccess.Money.Reconcilation
{
    [InjectAsSingleton]
    public class BalanceReconcilationDao : IBalanceReconcilationDao
    {
        private readonly IMoedeloDbExecutor dbExecutor;
        private readonly IMoedeloReadOnlyDbExecutor readOnlyDbExecutor;
        private readonly IReconciliationResultClient reconciliationResultClient;

        private const int ActualDays = 1; // Считаем, что после 1 суток сверка "протухла"

        public BalanceReconcilationDao(
            IMoedeloDbExecutor dbExecutor,
            IMoedeloReadOnlyDbExecutor readOnlyDbExecutor,
            IReconciliationResultClient reconciliationResultClient)
        {
            this.dbExecutor = dbExecutor;
            this.readOnlyDbExecutor = readOnlyDbExecutor;
            this.reconciliationResultClient = reconciliationResultClient;
        }

        public async Task<ReconciliationResult> GetLastAsync(int firmId, int? settlementAccountId)
        {
            var sql = BuildSql.From(ReconcilationQueries.GetLast).IncludeBlockIf("FilterBySettlementAccountId", settlementAccountId != null).ToString();
            var queryObject = new QueryObject(sql, new {firmId, settlementAccountId});

            var balanceReconcilation = await dbExecutor.FirstOrDefaultAsync<BalanceReconcilation>(queryObject).ConfigureAwait(false);
            if (balanceReconcilation == null)
            {
                return null;
            }

            if (IsNotActualReconciliation(balanceReconcilation))
            {
                return null;
            }

            var reconciliationResult = await reconciliationResultClient.GetAsync(balanceReconcilation.SessionId, firmId).ConfigureAwait(false);
            return new ReconciliationResult
            {
                Status = balanceReconcilation.Status,
                SessionId = balanceReconcilation.SessionId,
                ReconcilationDate = balanceReconcilation.ReconcilationDate,
                SettlementAccountId = settlementAccountId ?? 0,
                CreateDate = balanceReconcilation.CreateDate,
                ExcessOperations = reconciliationResult?.ExcessOperations?.Select(ReconciliationResultMapper.Map)
                    .OrderByDescending(x => x.Date).ToList() ?? new List<ReconciliationOperation>(),
                MissingOperations = reconciliationResult?.MissingOperations?.Select(ReconciliationResultMapper.Map)
                    .OrderByDescending(x => x.Date).ToList() ?? new List<ReconciliationOperation>()
            };
        }

        public async Task<ReconciliationResult[]> GetLastBySettlementAccountIdsAsync(int firmId, ReconciliationStatus reconciliationStatus, IReadOnlyCollection<int> settlementAccountIds)
        {
            if (settlementAccountIds == null || settlementAccountIds.Any() == false)
            {
                return Array.Empty<ReconciliationResult>();
            }

            var queryObject = new QueryObject(ReconcilationQueries.GetLastBySettlementAccountIds, new 
            { 
                FirmId = firmId,
                Status = reconciliationStatus,
                SettlementAccountIds = settlementAccountIds.ToIntListTVP()
            });

            var balanceReconcilations = await readOnlyDbExecutor.QueryAsync<BalanceReconcilation>(queryObject).ConfigureAwait(false);

            var result = new List<ReconciliationResult>();
            foreach (var balanceReconcilation in balanceReconcilations)
            {
                var reconciliationResult = await reconciliationResultClient
                    .GetAsync(balanceReconcilation.SessionId, firmId).ConfigureAwait(false);
                result.Add(new ReconciliationResult
                {
                    Status = balanceReconcilation.Status,
                    SessionId = balanceReconcilation.SessionId,
                    ReconcilationDate = balanceReconcilation.ReconcilationDate,
                    SettlementAccountId = balanceReconcilation.SettlementAccountId,
                    CreateDate = balanceReconcilation.CreateDate,
                    ExcessOperations = reconciliationResult?.ExcessOperations
                        ?.Select(ReconciliationResultMapper.Map).OrderByDescending(x => x.Date).ToList() ?? new List<ReconciliationOperation>(),
                    MissingOperations = reconciliationResult?.MissingOperations
                        ?.Select(ReconciliationResultMapper.Map).OrderByDescending(x => x.Date).ToList() ?? new List<ReconciliationOperation>()
                });
            }

            return result.ToArray();
        }

        public async Task<ReconciliationResult> GetBySessionIdAsync(int firmId, Guid sessionId)
        {
            var queryObject = new QueryObject(ReconcilationQueries.GetBySessionId, new { firmId, sessionId });
            var balanceReconcilation = await dbExecutor.FirstOrDefaultAsync<BalanceReconcilation>(queryObject)
                .ConfigureAwait(false);
            if (balanceReconcilation == null)
            {
                return null;
            }

            var reconciliationResult = await reconciliationResultClient
                .GetAsync(balanceReconcilation.SessionId, firmId).ConfigureAwait(false);

            if (reconciliationResult != null)
            {
                foreach (var result in reconciliationResult.ExcessOperations)
                {
                    result.Date = result.Date.ToLocalTime();
                }

                foreach (var result in reconciliationResult.MissingOperations)
                {
                    result.Date = result.Date.ToLocalTime();
                }
            }

            return new ReconciliationResult
            {
                Status = balanceReconcilation.Status,
                SessionId = balanceReconcilation.SessionId,
                ReconcilationDate = balanceReconcilation.ReconcilationDate,
                CreateDate = balanceReconcilation.CreateDate,
                SettlementAccountId = balanceReconcilation.SettlementAccountId,
                ExcessOperations = reconciliationResult?.ExcessOperations
                    ?.Select(ReconciliationResultMapper.Map).ToList() ?? new List<ReconciliationOperation>(),
                MissingOperations = reconciliationResult?.MissingOperations?.Select(ReconciliationResultMapper.Map)
                    .ToList() ?? new List<ReconciliationOperation>()
            };
        }

        public Task<DateTime?> GetMaxCreateDateAsync(int firmId, int settlementAccountId)
        {
            var queryObject = new QueryObject(ReconcilationQueries.GetMaxCreateDate, new { firmId, settlementAccountId });
            return dbExecutor.FirstOrDefaultAsync<DateTime?>(queryObject);
        }

        public Task InsertAsync(int firmId, BalanceReconcilation reconcilation)
        {
            var insertTask = reconciliationResultClient.InsertAsync(new ReconciliationResultDto
            {
                SessionId = reconcilation.SessionId,
                FirmId = firmId,
                ExcessOperations = new List<ReconciliationOperationDto>(),
                MissingOperations = new List<ReconciliationOperationDto>()
            });

            var dbParam = new
            {
                firmId,
                reconcilation.ServiceBalance,
                reconcilation.BankBalance,
                reconcilation.ReconcilationDate,
                reconcilation.CreateDate,
                reconcilation.SessionId,
                reconcilation.Status,
                reconcilation.SettlementAccountId
            };
            var queryObject = new QueryObject(ReconcilationQueries.Insert, dbParam);
            var dbTask = dbExecutor.ExecuteAsync(queryObject);

            return Task.WhenAll(insertTask, dbTask);
        }

        public Task SetStatusAsync(int firmId, Guid sessionId, ReconciliationStatus status)
        {
            var param = new
            {
                firmId,
                sessionId,
                status
            };
            var queryObject = new QueryObject(ReconcilationQueries.SetStatus, param);
            return dbExecutor.ExecuteAsync(queryObject);
        }

        public Task SetReadyToCompleteAsync(int firmId, int settlementAccountId)
        {
            var param = new
            {
                firmId,
                settlementAccountId,
                completeStatus = ReconciliationStatus.Completed,
                readyStatus = ReconciliationStatus.Ready
            };
            var queryObject = new QueryObject(ReconcilationQueries.SetReadyToComplete, param);
            return dbExecutor.ExecuteAsync(queryObject);
        }

        public Task UpdateAsync(int firmId, ReconciliationResult reconciliationResult)
        {
            return reconciliationResultClient.UpdateAsync(new ReconciliationResultDto
            {
                SessionId = reconciliationResult.SessionId,
                FirmId = firmId,
                ExcessOperations = reconciliationResult.ExcessOperations.Select(ReconciliationResultMapper.Map).ToList(),
                MissingOperations = reconciliationResult.MissingOperations.Select(ReconciliationResultMapper.Map).ToList()
            });
        }

        public Task<bool> IsAnyInProgressAsync(int firmId, int settlementAccountId)
        {
            var queryObject = new QueryObject(ReconcilationQueries.IsAnyInProgress, new { firmId, settlementAccountId, inProgressStatus = ReconciliationStatus.InProgress });
            return dbExecutor.FirstOrDefaultAsync<bool>(queryObject);

        }

        public Task<Guid?> GetLastSessionInProcessAsync(int firmId, int settlementAccountId)
        {
            var queryObject = new QueryObject(ReconcilationQueries.GetLastSessionIdInProcess, new { firmId, settlementAccountId, inProgressStatus = ReconciliationStatus.InProgress, DayAgo = DateTime.Now.AddDays(-1) });
            return dbExecutor.FirstOrDefaultAsync<Guid?>(queryObject);
        }

        public Task<List<BalanceReconcilation>> GetByDateAsync(int firmId, IReadOnlyCollection<int> settlementAccountIds, DateTime date)
        {
            var sql = BuildSql.From(ReconcilationQueries.GetByDate).ToString();
            var queryObject = new QueryObject(sql, new {firmId, settlementAccountIds, date});

            return dbExecutor.QueryAsync<BalanceReconcilation>(queryObject);
        }

        public async Task<ISet<int>> GetSettlementAccountsInProgressAsync(int firmId, IReadOnlyCollection<int> settlementAccountIds)
        {
            var param = new
            {
                firmId,
                SettlementAccountIds = settlementAccountIds.ToIntListTVP(),
                InProgressStatus = ReconciliationStatus.InProgress,
                ActualDays = DateTime.Today.AddDays(-ActualDays)
            };
            var queryObject = new QueryObject(ReconcilationQueries.GetSettlementAccountsInProgress, param);
            var result = await  dbExecutor.QueryAsync<int>(queryObject).ConfigureAwait(false);
            return new HashSet<int>(result);
        }

        private static bool IsNotActualReconciliation(BalanceReconcilation data)
        {
            return data.Status == ReconciliationStatus.InProgress &&
                   data.CreateDate <= DateTime.Today.AddDays(-ActualDays);
        }
    }
}
