using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Consultations.Dto.ConsultationsPeriod;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.Consultations.Client.ConsultationsPeriod
{
    [InjectAsSingleton]
    public class ConsultationsPeriodClient : BaseApiClient, IConsultationsPeriodClient
    {
        private readonly ISettingRepository settingRepository;
        private const string ControllerName = "/ConsultationsPeriod/";

        public ConsultationsPeriodClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("ConsultationsApiEndpoint") + ControllerName;
        }

        public Task<List<ConsultationsPeriodDto>> GetAllPeriodsAsync()
        {
            return GetAsync<List<ConsultationsPeriodDto>>("GetAllPeriods");
        }

        public Task SetAllPeriodsAsync(BasePeriodsRequestDto request)
        {
            return PostAsync("SetAllPeriods", request);
        }

        public Task SetPeriodsAsync(SetPeriodsRequestDto request)
        {
            return PostAsync("SetPeriods", request);
        }
    }
}