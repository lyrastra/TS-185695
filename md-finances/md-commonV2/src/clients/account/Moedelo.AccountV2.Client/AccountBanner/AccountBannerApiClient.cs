using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.AccountV2.Dto.AccountBanner;
using Moedelo.Common.Enums.Enums.Account;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.AccountBanner
{
    [InjectAsSingleton]
    public class AccountBannerApiClient : BaseApiClient, IAccountBannerApiClient
    {
        private readonly SettingValue apiEndPoint;

        public AccountBannerApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/AccountBanner/V2";
        }

        public Task<List<AccountBannerDto>> GetByAccountTypeAsync(AccountType accountType, int firmId, int userId)
        {
            return GetAsync<List<AccountBannerDto>>("/GetByAccountType", new {accountType, firmId, userId});
        }

        public Task<List<AccountBannerDto>> GetAllAsync(int firmId, int userId)
        {
            return GetAsync<List<AccountBannerDto>>("/GetAll", new {firmId, userId});
        }

        public Task<AccountBannerDto> GetByIdAsync(long id, int firmId, int userId)
        {
            return GetAsync<AccountBannerDto>("/GetById", new {id, firmId, userId});
        }

        public Task<long> SaveAsync(AccountBannerDto accountBannerDto, int firmId, int userId)
        {
            return PostAsync<AccountBannerDto, long>($"/Save?firmId={firmId}&userId={userId}", accountBannerDto);
        }

        public Task Delete(long id, int firmId, int userId)
        {
            return PostAsync($"/Delete?firmId={firmId}&userId={userId}");
        }
    }
}