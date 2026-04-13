using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Contractors
{
    [InjectAsSingleton(typeof(IMoneyContractorDao))]
    public class MoneyContractorDao : IMoneyContractorDao
    {
        private readonly IMoedeloReadOnlyDbExecutor dbExecutor;

        public MoneyContractorDao(IMoedeloReadOnlyDbExecutor dbExecutor)
        {
            this.dbExecutor = dbExecutor;
        }

        public Task<List<Contractor>> GetAsync(int firmId, MoneyContractorType type, CancellationToken cancellationToken)
        {
            var param = new
            {
                firmId,
                kontragentType = MoneyContractorType.Kontragent,
                workerType = MoneyContractorType.Worker,
                type
            };
            var queryObject = new QueryObject(ContractorQueries.GetWithRating, param)
                .WithAuditTrailSpanName("MoneyContractorDao.Get");
            return dbExecutor.QueryAsync<Contractor>(queryObject, cancellationToken: cancellationToken);
        }
    }
}