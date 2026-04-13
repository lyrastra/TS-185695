using System;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.ClosedPeriods;
using Moedelo.Common.Enums.Enums.ClosingPeriod;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.ClosedPeriods
{
    public interface IClosedPeriodApiClient : IDI
    {
        Task<DateTime> GetLastClosedDateAsync(int firmId, int userId);
        Task CloseMonthAsync(int firmId, int userId, DateTime upToDate, CloseType? closeType = CloseType.Default);
        Task<int?> GetLastNotClosedPeriodEventIdAsync(int firmId, int userId, DateTime endDate);
        Task CloseYearAsync(int firmId, int userId, int year);
        Task<CheckClosingMonthDto> CheckClosingMonthAsync(int firmId, int userId, int month, int year);
        Task OpenPeriodAsync(int firmId, int userId, DateTime date);
    }
}
