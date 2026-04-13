using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.ProductionCalendar;

namespace Moedelo.PayrollV2.Client.ProductionCalendar
{
    [InjectAsSingleton]
    public class ProductionCalendarApiClient : BaseApiClient, IProductionCalendarApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public ProductionCalendarApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }
          
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/ProductionCalendar";
        }

        public Task<DateTime> GetWorkingDayByDateAsync(DateTime date)
        {
            return GetAsync<DateTime>("/GetWorkingDayByDate", new { date });
        }

        public Task<ProductionCalendarDayType> GetTypeByDateAsync(DateTime date)
        {
            return GetAsync<ProductionCalendarDayType>("/GetTypeByDate", new { date });
        }

        public Task<ProductionCalendarDayDto> GetFirstDayOneOfTypesAfterDateAsync(DateTime date, int onNthDay,
            IReadOnlyCollection<ProductionCalendarDayType> dayTypes)
        {
            return GetAsync<ProductionCalendarDayDto>("/GetFirstDayOneOfTypesAfterDate", new {date, onNthDay, dayTypes});
        }
    }
}
