using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.ClosedPeriods;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.ClosedPeriods
{
    public interface IClosedPeriodsDao
    {
        Task<DateTime?> GetLastClosedDateAsync(int firmId, CancellationToken ctx);

        Task<List<MoneyDocumentsCount>> GetNonProvidedInAccountingOrderCountsAsync(int firmId, DateTime startDate,
            DateTime endDate, CancellationToken cancellationToken);

        Task<List<MoneyDocument>> GetNonProvidedInAccountingOrdersAsync(int firmId, DateTime startDate,
            DateTime endDate, CancellationToken cancellationToken);
    }
}