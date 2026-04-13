using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto;
using Moedelo.RptV2.Dto.PfrFixes;

namespace Moedelo.RptV2.Client.PfrFixes
{
    [InjectAsSingleton]
    public class PfrFixesApiClient : BaseApiClient, IPfrFixesApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public PfrFixesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) 
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        public async Task<FileResponseDto> GetRegistryAsync(int userId, int firmId)
        {
            return await GetAsync<FileResponseDto>("/PfrFixes/GetRegistry", new { firmId, userId }).ConfigureAwait(false);
        }

        public async Task<List<PfrFixesWizardDataDto>> GetCompletedWizardDataByYearAsync(int userId, int firmId, int year)
        {
            return await GetAsync<List<PfrFixesWizardDataDto>>("/PfrFixes/GetCompletedWizardDataByYear", new { firmId, userId, year })
                .ConfigureAwait(false);
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
