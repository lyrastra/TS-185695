using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.FrameInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.FrameInfo
{
    [InjectAsSingleton]
    public class AccountFrameInfoApiClient : BaseApiClient, IAccountFrameInfoApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public AccountFrameInfoApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<AccountFrameInfoForUserWithFirmIdsResponseDto> GetForUserWithFirmIdsByIdAsync(int id)
        {
            return GetAsync<AccountFrameInfoForUserWithFirmIdsResponseDto>("/Rest/AccountFrameInfo/GetForUserWithFirmIdsById", new {id});
        }

        public Task<AccountFrameInfoForUserWithFirmIdsResponseDto> GetForUserWithFirmIdsByLoginAsync(string login)
        {
            return GetAsync<AccountFrameInfoForUserWithFirmIdsResponseDto>("/Rest/AccountFrameInfo/GetForUserWithFirmIdsByLogin", new {login});
        }

        public Task<AccountFrameInfoForFirmResponseDto> GetForFirmByIdAsync(int id)
        {
            return GetAsync<AccountFrameInfoForFirmResponseDto>("/Rest/AccountFrameInfo/GetForFirmById", new {id});
        }

        public Task<AccountFrameInfoForFirmResponseDto> GetForFirmByInnAsync(string inn)
        {
            return GetAsync<AccountFrameInfoForFirmResponseDto>("/Rest/AccountFrameInfo/GetForFirmByInn", new {inn});
        }

        public Task<List<AccountFrameInfoForFirmResponseDto>> GetForFirmsByIdsAsync(IReadOnlyCollection<int> ids)
        {
            ids = ids.AsSet();
            return PostAsync<IReadOnlyCollection<int>, List<AccountFrameInfoForFirmResponseDto>>("/Rest/AccountFrameInfo/GetForFirmsByIds", ids);
        }
    }
}