using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OutSystemsIntegrationV2.Dto.Vacancies;

namespace Moedelo.OutSystemsIntegrationV2.Client.Vacancies
{
    [InjectAsSingleton]
    public class VacanciesClient : BaseApiClient, IVacanciesClient
    {
        private readonly SettingValue apiEndpoint;

        public VacanciesClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("OutSystemsIntegrationApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<VacanciesDto> GetVacanciesByInnAsync(string inn, int? limit, int? offset, TimeSpan? timeout = null)
        {
            var setting = timeout.HasValue ? new HttpQuerySetting(timeout) : null;
            return GetAsync<VacanciesDto>("/Vacancies/V1/WorkVacancies", new { inn, limit, offset }, setting: setting);
        }

        public Task<VacanciesDto> GetVacancyAsync(Guid id, string companyCode)
        {
            return GetAsync<VacanciesDto>("/Vacancies/V1/WorkVacancy", new { id, companyCode });
        }
    }
}