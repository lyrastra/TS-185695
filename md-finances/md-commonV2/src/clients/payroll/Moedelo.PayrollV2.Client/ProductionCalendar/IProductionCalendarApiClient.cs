using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.ProductionCalendar;

namespace Moedelo.PayrollV2.Client.ProductionCalendar
{
    public interface IProductionCalendarApiClient : IDI
    {
        Task<DateTime> GetWorkingDayByDateAsync(DateTime date);
        
        Task<ProductionCalendarDayType> GetTypeByDateAsync(DateTime date);
        
        Task<ProductionCalendarDayDto> GetFirstDayOneOfTypesAfterDateAsync(DateTime date, int onNthDay,
            IReadOnlyCollection<ProductionCalendarDayType> types);
    }
}
