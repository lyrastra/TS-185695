using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.FrameInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.FrameInfo
{
    [InjectAsSingleton]
    public class FrameInfoApiClient : BaseApiClient, IFrameInfoApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FrameInfoApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }

        public Task<FrameInfoForUserResponseDto> GetFrameInfoForUserByIdAsync(int id)
        {
            return GetAsync<FrameInfoForUserResponseDto>("/Rest/FrameInfo/GetFrameInfoForUserById", new {id});
        }

        public Task<FrameInfoForUserResponseDto> GetFrameInfoForUserByLoginAsync(string login)
        {
            return GetAsync<FrameInfoForUserResponseDto>("/Rest/FrameInfo/GetFrameInfoForUserByLogin", new {login});
        }

        public Task<FrameInfoForFirmResponseDto> GetFrameInfoForFirmByIdAsync(int id)
        {
            return GetAsync<FrameInfoForFirmResponseDto>("/Rest/FrameInfo/GetFrameInfoForFirmById", new {id});
        }

        public Task<FrameInfoForFirmResponseDto> GetFrameInfoForFirmByInnAsync(string inn)
        {
            return GetAsync<FrameInfoForFirmResponseDto>("/Rest/FrameInfo/GetFrameInfoForFirmByInn", new {inn});
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}