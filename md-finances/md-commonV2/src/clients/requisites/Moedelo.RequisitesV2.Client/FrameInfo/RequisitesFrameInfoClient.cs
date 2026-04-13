using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.FrameInfo;

namespace Moedelo.RequisitesV2.Client.FrameInfo
{
    [InjectAsSingleton]
    public class RequisitesFrameInfoClient : BaseApiClient, IRequisitesFrameInfoClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RequisitesFrameInfoClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<RequisitesFrameInfoForFirmResponseDto> GetForFirmByIdAsync(int id)
        {
            return GetAsync<RequisitesFrameInfoForFirmResponseDto>("/RequisitesFrameInfo/GetForFirmById", new {id});
        }

        public Task<RequisitesFrameInfoForFirmResponseDto> GetForFirmByInnAsync(string inn)
        {
            return GetAsync<RequisitesFrameInfoForFirmResponseDto>("/RequisitesFrameInfo/GetForFirmByInn", new {inn});
        }

        public Task<List<RequisitesFrameInfoForFirmResponseDto>> GetForFirmsByIdsAsync(List<int> ids)
        {
            return PostAsync<List<int>, List<RequisitesFrameInfoForFirmResponseDto>>("/RequisitesFrameInfo/GetForFirmsByIds", ids);
        }
    }
}