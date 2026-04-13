using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto;
using Moedelo.ErptV2.Dto.EReportRequisites;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.EReportRequisites
{
    [InjectAsSingleton]
    public class EReportRequisitesApiClient : BaseApiClient, IEReportRequisitesApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public EReportRequisitesApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<EReportRequisitesDto> Get(int firmId)
        {
            return GetAsync<EReportRequisitesDto>($"/Requisites/Get/{firmId}");
        }

        public Task<List<FnsData>> GetAdditionalFnsAsync(int firmId)
        {
            return GetAsync<List<FnsData>>($"/Requisites/GetAdditionalFns/{firmId}");
        }
        
        public Task<BaseDto> AddAdditionalFnsAsync(AdditionalFnsData fnsData)
        {
            return PostAsync<AdditionalFnsData, BaseDto>("/Requisites/AddAdditionalFns", fnsData); 
        }

        public Task ResetAdditionalFnsAsync(int firmId)
        {
            return PostAsync($"/Requisites/ResetAdditionalFns?firmId={firmId}"); 
        }
        
        public Task DeleteAdditionalFnsAsync(int firmId, string tax, string kpp)
        {
            return PostAsync($"/Requisites/DeleteAdditionalFns?firmId={firmId}&tax={tax}&kpp={kpp}"); 
        }
    }
}