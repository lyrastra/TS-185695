using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.ApiProxy.Client.CaseInfo
{
    [InjectAsSingleton]
    public class BpmCaseInfoApiProxyClient : BaseApiClient, IBpmCaseInfoApiProxyClient
    {
        private readonly SettingValue apiEndpoint;

        public BpmCaseInfoApiProxyClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        public Task<int> GetOpenedCountAsync(int firmId) => GetAsync<int>("/Rest/CaseInfo/Opened/Count", new {firmId});

        public Task<int> GetOpenedCountAsync(string login) => GetAsync<int>("/Rest/CaseInfo/Opened/Count", new {login});

        public Task<List<CaseInfoDto>> GetOpenedInfoAsync(int firmId, int count = 20, int? afterNumber = null, bool orderAsc = false)
            => GetAsync<List<CaseInfoDto>>("/Rest/CaseInfo/Opened", new {firmId, count, afterNumber, orderAsc});
        
        public Task<List<CaseInfoDto>> GetOpenedInfoAsync(string login, int count = 20, int? afterNumber = null, bool orderAsc = false)
            => GetAsync<List<CaseInfoDto>>("/Rest/CaseInfo/Opened", new {login, count, afterNumber, orderAsc});

        public Task<List<CaseInfoDto>> CasesFeedStartAsync(int firmId)
            => GetAsync<List<CaseInfoDto>>("/Rest/CaseInfo/CasesFeedStart", new {firmId});

        public Task<List<CaseInfoDto>> CasesFeedAsync(int firmId, int caseNumber, DateTime date)
            => GetAsync<List<CaseInfoDto>>("/Rest/CaseInfo/CasesFeed", new {firmId, caseNumber, date});

        public Task<CaseDocumentsDto[]> GetDocumentsAsync(string[] caseIds)
            => PostAsync<string[], CaseDocumentsDto[]>("/Rest/CaseInfo/Documents", caseIds);

        public Task<List<CaseMessageDto>> CaseMessagesAsync(string caseId, int count = 20, DateTime? date = null, 
            string lastId = null, bool orderAsc = false, int? firmId = null)
            => GetAsync<List<CaseMessageDto>>("/Rest/CaseInfo/CaseMessages", new {caseId, count, date, lastId, orderAsc, firmId});

        public Task<CaseInfoDto> GetCaseInfoAsync(string caseId, int? firmId = null)
            => GetAsync<CaseInfoDto>("/Rest/CaseInfo/Get", new {caseId, firmId});

        public Task<CaseMessageDto> GetCaseMessageInfoAsync(string caseMessageId, int? firmId = null)
            => GetAsync<CaseMessageDto>("/Rest/CaseInfo/GetMessageInfo", new {caseMessageId, firmId});

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/apiproxy";
        }
    }
}