using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ProductionCalendars;
using Moedelo.Payroll.Enums.ProductionCalendars;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IProductionCalendarApiClient))]
    internal sealed class ProductionCalendarApiClient : BaseLegacyApiClient, IProductionCalendarApiClient
    {
        public ProductionCalendarApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ProductionCalendarApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<ProductionCalendarDayDto[]> GetAsync(DateTime startDate, DateTime endDate)
        {
            return GetAsync<ProductionCalendarDayDto[]>("/ProductionCalendar", new {startDate, endDate});
        }

        public Task<DateTime> GetWorkingDayByDateAsync(DateTime date)
        {
            return GetAsync<DateTime>("/ProductionCalendar/GetWorkingDayByDate", new {date});
        }

        public Task<ProductionCalendarDayType> GetTypeByDateAsync(DateTime date)
        {
            return GetAsync<ProductionCalendarDayType>("/ProductionCalendar/GetTypeByDate", new {date});
        }

        public Task<ProductionCalendarDayDto> GetFirstDayOneOfTypesAfterDateAsync(DateTime date, int onNthDay,
            IReadOnlyCollection<ProductionCalendarDayType> dayTypes)
        {
            return GetAsync<ProductionCalendarDayDto>("/ProductionCalendar/GetFirstDayOneOfTypesAfterDate",
                new
                {
                    date = date, 
                    onNthDay = onNthDay, 
                    dayTypes = dayTypes.ToArray()
                });
        }

        public Task<ProductionCalendarDayDto> GetFirstDayOneOfTypesBeforeDateAsync(DateTime date, int onNthDay, IReadOnlyCollection<ProductionCalendarDayType> dayTypes)
        {
            return GetAsync<ProductionCalendarDayDto>("/ProductionCalendar/GetFirstDayOneOfTypesBeforeDate",
                new
                {
                    date = date, 
                    onNthDay = onNthDay, 
                    dayTypes = dayTypes.ToArray()
                });
        }

        public Task<DateTime> GetWorkingDayBeforeByDateAsync(DateTime date)
        {
            return GetAsync<DateTime>("/ProductionCalendar/GetWorkingDayBeforeByDate", new { date });
        }
    }
}