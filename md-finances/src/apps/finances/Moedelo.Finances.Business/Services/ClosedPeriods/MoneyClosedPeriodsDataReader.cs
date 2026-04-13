using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.ClosedPeriods;
using Moedelo.Finances.Domain.Interfaces.DataAccess.ClosedPeriods;
using Moedelo.Finances.Domain.Models.ClosedPeriods;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.ClosedPeriods
{
    [InjectAsSingleton(typeof(IMoneyClosedPeriodsDataReader))]
    public class MoneyClosedPeriodsDataReader(IClosedPeriodsDao dao) : IMoneyClosedPeriodsDataReader
    {
        public Task<List<MoneyDocumentsCount>> GetNonProvidedInAccountingOrderCountsAsync(IUserContext userContext,
            DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return dao.GetNonProvidedInAccountingOrderCountsAsync(userContext.FirmId, startDate, endDate, cancellationToken);
        }
        
        public Task<List<MoneyDocument>> GetNonProvidedInAccountingOrdersAsync(IUserContext userContext,
            DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return dao.GetNonProvidedInAccountingOrdersAsync(userContext.FirmId, startDate, endDate, cancellationToken);
        }
    }
}