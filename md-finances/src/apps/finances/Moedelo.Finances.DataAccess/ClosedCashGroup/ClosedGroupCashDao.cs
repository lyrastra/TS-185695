using System;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Interfaces.DataAccess.CloseCashGroup;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.ClosedCashGroup
{
    [InjectAsSingleton]
    public class ClosedGroupCashDao : ICloseCashGroupDao
    {
        private readonly IMoedeloReadOnlyDbExecutor dbExecutor;

        public ClosedGroupCashDao(IMoedeloReadOnlyDbExecutor dbExecutor)
        {
            this.dbExecutor = dbExecutor;
        }

        public Task<DateTime?> GetLastClosedCashDateAsync(int firmId)
        {
            var query = new QueryObject(Sql.GetLastClosedCashDate, new { firmId });
            return dbExecutor.FirstOrDefaultAsync<DateTime?>(query);
        }
    }
}
