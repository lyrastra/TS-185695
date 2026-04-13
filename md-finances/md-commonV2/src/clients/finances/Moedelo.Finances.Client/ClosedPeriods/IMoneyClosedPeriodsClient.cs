using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Dto.ClosedPeriods;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Client.ClosedPeriods
{
    public interface IMoneyClosedPeriodsClient : IDI
    {
        Task<DateTime> GetLastClosedDateAsync(int firmId, int userId);
        Task<List<MoneyDocumentsCountDto>> CountOrdersNonProvidedInAccountingAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
        Task<List<MoneyDocumentsCountDto>> OrdersNonProvidedInAccountingAsync(int firmId, int userId,
            DateTime startDate, DateTime endDate);
    }
}