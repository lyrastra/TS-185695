using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.UserLoginLog.Dto;

namespace Moedelo.UserLoginLog.Client
{
    [InjectAsSingleton]
    public class UserLoginLogClient : BaseApiClient, IUserLoginLogClient
    {
        private readonly SettingValue apiEndpoint;
        
        public UserLoginLogClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("UserLoginLogApiEndpoint");
        }

        public Task<List<UserLastLoginDateDto>> GetLastLoginDateByUserIdsAsync(LastLoginDatesRequestDto requestDto)
        {
            return PostAsync<LastLoginDatesRequestDto, List<UserLastLoginDateDto>>("/LastLoginDate", requestDto);
        }

        public Task<IReadOnlyCollection<UserLoginSummaryResponseDto>> GetLoginSummaryByPlatformsAndPeriod(UserLoginSummaryRequestDto request)
        {
            return PostAsync<UserLoginSummaryRequestDto, IReadOnlyCollection<UserLoginSummaryResponseDto>>(
                "/LoginSummaryByPlatformsAndPeriod", 
                request
            );
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}