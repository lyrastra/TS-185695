using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.PostgreSqlDataAccess.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Interface;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Models;
using Moedelo.Money.BankBalanceHistory.DataAccess.Abstractions;
using Moedelo.Money.BankBalanceHistory.DataAccess.Balances.DbModels;
using Moedelo.Money.BankBalanceHistory.DataAccess.Extensions;
using Moedelo.Money.BankBalanceHistory.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Helpers;

namespace Moedelo.Money.BankBalanceHistory.DataAccess.Balances
{
    [InjectAsSingleton(typeof(IBalancesDao))]
    internal class BalancesDao : MoedeloPostgreSqlDbExecutorBase, IBalancesDao
    {
        private readonly ISqlScriptReader scriptReader;

        public BalancesDao(
            ISqlScriptReader scriptReader,
            IPostgreSqlExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetBankBalanceHistoryConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<bool> IsContainsDataForPeriodAsync(int firmId, int settlementAccountId, DateTime startDate, DateTime endDate, bool includeUserMovement)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "Balances.Scripts.IsContainsDataForPeriod.sql"))
                .IncludeLineIf("IsNotUserMovementFilter", !includeUserMovement)
                .ToString();
            
            var param = new
            {
                firmId,
                settlementAccountId,
                StartDate = startDate.Date,
                EndDate = endDate.Date
            };
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<bool>(queryObject);
        }

        public Task<BankBalanceResponse> GetAsync(int firmId, BankBalanceRequest request)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "Balances.Scripts.Select.sql"))
                .IncludeLineIf("IsNotUserMovementFilter", !request.IncludeUserMovement)
                .ToString();
            
            var param = new
            {
                firmId,
                request.SettlementAccountId,
                request.StartDate,
                request.EndDate
            };
            var queryObject = new QueryObject(sql, param);
            return FirstOrDefaultAsync<BankBalanceResponse>(queryObject);
        }

        public async Task<LastBankBalance[]> GetOnDateByFirmIdAsync(int firmId, DateTime onDate, DateTime minDate, bool includeUserMovement)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "Balances.Scripts.SelectOnDateByFirmId.sql"))
                .IncludeLineIf("IsNotUserMovementFilter", !includeUserMovement)
                .ToString();
            
            var param = new
            {
                firmId, 
                onDate = onDate.Date, 
                minDate = minDate.Date
            };
            var queryObject = new QueryObject(sql, param);
            var dbModels = await QueryAsync<LastFirmBankBalanceDbModel>(queryObject);
            return dbModels.Select(Map).ToArray();
        }

        public async Task<IReadOnlyDictionary<int, LastBankBalance[]>> GetOnDateByFirmIdsAsync(IReadOnlyCollection<int> firmIds,
            DateTime onDate, DateTime minDate, bool includeUserMovement)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "Balances.Scripts.SelectOnDateByFirmIds.sql"))
                .IncludeLineIf("IsNotUserMovementFilter", !includeUserMovement)
                .ToString();
            
            var param = new
            {
                onDate = onDate.Date, 
                minDate = minDate.Date
            };
            var tempTable = firmIds.ToTempIntIds("firm_ids", true);
            var queryObject = new QueryObject(sql, param, tempTable);
            var dbModels = await QueryAsync<LastFirmBankBalanceDbModel>(queryObject);
            return dbModels.GroupBy(x => x.FirmId).ToDictionary(x => x.Key, x => x.Select(Map).ToArray());
        }

        public Task UpdateAsync(int firmId, IReadOnlyCollection<BankBalanceUpdateRequest> requests)
        {
            var sql = scriptReader.Get(this, "Balances.Scripts.Save.sql");
            var tempTable = requests.Select(x => Map(firmId, x)).ToTemporaryTable("balances", true);
            var queryObject = new QueryObject(sql, null, tempTable);
            return ExecuteAsync(queryObject);
        }

        public Task CleanUpByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            var sql = scriptReader.Get(this, "Balances.Scripts.DeleteByFirmIds.sql");
            var tempTable = firmIds.ToTempIntIds("firm_ids", true);
            var queryObject = new QueryObject(sql, null, tempTable);
            return ExecuteAsync(queryObject);
        }

        private static BalanceUpdateDbModel Map(int firmId, BankBalanceUpdateRequest request)
        {
            return new BalanceUpdateDbModel
            {
                FirmId = firmId,
                SettlementAccountId = request.SettlementAccountId,
                BalanceDate = request.BalanceDate.Date,
                StartBalance = request.StartBalance,
                EndBalance = request.EndBalance,
                IncomingBalance = request.IncomingBalance,
                OutgoingBalance = request.OutgoingBalance,
                IsUserMovement = request.IsUserMovement
            };
        }

        private static LastBankBalance Map(LastFirmBankBalanceDbModel request)
        {
            return new LastBankBalance
            (
                request.SettlementAccountId,
                request.Balance,
                request.BalanceDate.Date,
                request.ModifyDate ?? request.BalanceDate.Date
            );
        }
    }
}
