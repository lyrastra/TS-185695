using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Reconcilation;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.PayrollV2.Client.Employees;

namespace Moedelo.Finances.Business.Services.Money.Sources.Readers
{
    [InjectAsSingleton]
    public class ReconciliationDataReader: IReconciliationDataReader
    {
        private readonly IEmployeesApiClient employeesApiClient;
        private readonly IBalanceReconcilationDao balanceReconcilationDao;

        public ReconciliationDataReader(IEmployeesApiClient employeesApiClient, IBalanceReconcilationDao balanceReconcilationDao)
        {
            this.employeesApiClient = employeesApiClient;
            this.balanceReconcilationDao = balanceReconcilationDao;
        }


        public async Task<Dictionary<long, ReconciliationData>> GetReconciliationDataAsync(int firmId, int userId, IReadOnlyCollection<MoneySource> sources)
        {
            var settlementAccountSources = sources.Where(x => x.Type == MoneySourceType.SettlementAccount).ToArray();
            if (settlementAccountSources.Length == 0)
            {
                return new Dictionary<long, ReconciliationData>();
            }

            var settlementAccountIds = settlementAccountSources.Select(x => (int)x.Id).ToArray();
            var settlementAccountsInProgressTask = balanceReconcilationDao.GetSettlementAccountsInProgressAsync(firmId, settlementAccountIds);
            var workersTask = employeesApiClient.GetNotFiredWorkersAsync(firmId, userId, null, null);
            await Task.WhenAll(settlementAccountsInProgressTask, workersTask).ConfigureAwait(false);
            var hasWorkers = workersTask.Result != null && workersTask.Result.Count > 0;
            return settlementAccountSources.ToDictionary(x => x.Id, x =>
                new ReconciliationData
                {
                    IsReconciliationInProcess = settlementAccountsInProgressTask.Result.Contains((int)x.Id),
                    HasEmployees = hasWorkers
                });
        }
    }
}
