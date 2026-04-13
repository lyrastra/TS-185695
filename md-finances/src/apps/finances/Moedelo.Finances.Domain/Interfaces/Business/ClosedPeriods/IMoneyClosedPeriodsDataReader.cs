using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.ClosedPeriods;
using Moedelo.CommonV2.UserContext.Domain;

namespace Moedelo.Finances.Domain.Interfaces.Business.ClosedPeriods;

public interface IMoneyClosedPeriodsDataReader
{
    Task<List<MoneyDocumentsCount>> GetNonProvidedInAccountingOrderCountsAsync(IUserContext userContext,
        DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

    Task<List<MoneyDocument>> GetNonProvidedInAccountingOrdersAsync(IUserContext userContext, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken);
}