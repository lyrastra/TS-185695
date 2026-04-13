using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.Reports.DataAccess.Abstractions.Balances;
using Moedelo.Money.Reports.DataAccess.Abstractions.Balances.Models;
using Moedelo.Money.Reports.DataAccess.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Reports.DataAccess.Balances
{
    [InjectAsSingleton(typeof(IBalancesDao))]
    internal class BalancesDao : MoedeloSqlDbExecutorBase, IBalancesDao
    {
        private static readonly OperationState[] unrecognizedOperationStates = new[]
        {
            OperationState.MissingKontragent,
            OperationState.MissingWorker,
            OperationState.MissingExchangeRate,
            OperationState.MissingCurrencySettlementAccount,
            OperationState.MissingContract,
            OperationState.MissingCommissionAgent
        };

        private readonly ISqlScriptReader scriptReader;

        public BalancesDao(
            ISettingRepository settings,
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetReadOnlyConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }

        public Task<IReadOnlyList<SettlementAccountBalanceResponse>> GetAsync(SettlementAccountBalancesRequest request)
        {
            var sqlTemplate = scriptReader.Get(this, "Balances.Scripts.GetSettlementAccountBalances.sql");
            var param = new
            {
                request.OnDate,
                unrecognizedOperationStates,
                IncomingDirection = MoneyDirection.Incoming,
                OutgoingDirection = MoneyDirection.Outgoing,
                DefaultOperationState = OperationState.Default,
                ImportedOperationState = OperationState.Imported,
                OutsourceApprovedOperationState = OperationState.OutsourceApproved,
                PayedDocumentStatus = PaymentStatus.Payed
            };
            var tempTable = request.FirmInitDates.ToTemporaryTable("firmInitDates");
            var queryObject = new QueryObject(sqlTemplate, param, tempTable);
            return QueryAsync<SettlementAccountBalanceResponse>(queryObject, new QuerySetting(timeoutSeconds: 300));
        }
    }
}
