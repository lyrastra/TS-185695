using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class CrmKayakoApiClient : BaseApiClient, ICrmKayakoApiClient
    {
        private readonly HttpQuerySetting setting = new HttpQuerySetting {Timeout = new TimeSpan(0, 0, 10, 0)};
        private readonly SettingValue apiEndPoint;

        public CrmKayakoApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SuiteCrmApiUrl");
        }

        public Task<bool> GetFiles(DateTime date)
        {
            return GetAsync<bool>("/Kayako/GetFiles", new {date = date.ToString("dd.MM.yyyy")});
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}