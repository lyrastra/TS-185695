using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.Account;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.Account
{
    [InjectAsSingleton(typeof(IAccountApiClient))]
    public class AccountApiClient : BaseApiClient, IAccountApiClient
    {
        private readonly SettingValue apiEndPoint;

        public AccountApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountPrivateApiEndpoint") ;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/Account";
        }

        public Task<AccountDto> GetAccountByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return GetAsync<AccountDto>($"/V2/GetByUserId?userId={userId}", cancellationToken: cancellationToken);
        }

        public Task<Dictionary<int, AccountDto>> GetAccountsByUserIdsAsync(IReadOnlyCollection<int> userIds, CancellationToken cancellationToken = default)
        {
            const string uri = "/V2/GetByUserIds";

            if (userIds?.Any() != true)
            {
                return Task.FromResult(new Dictionary<int, AccountDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, Dictionary<int, AccountDto>>(uri, userIds, cancellationToken: cancellationToken);
        }

        public Task<List<AccountDto>> GetByIdsAsync(IReadOnlyCollection<int> accountIds)
        {
            if (accountIds?.Any() != true)
            {
                return Task.FromResult(new List<AccountDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<AccountDto>>("/V2/GetByIds", accountIds);
        }

        public Task<int> SaveUnionRequestAsync(UnionRequestDto unionRequest)
        {
            return PostAsync<UnionRequestDto, int>("/V2/SaveUnionRequest", unionRequest);
        }

        public Task DeleteAccountAsync(int accountId)
        {
            return PostAsync($"/V2/DeleteAccount?accountId={accountId}");
        }

        public Task<bool> CanSendUnionRequestAsync(int firmId, int userId)
        {
            return GetAsync<bool>("/V2/CanSendUnionRequest", new {userId, firmId});
        }

        public Task<int> SaveAccountAsync(AccountDto accountDto)
        {
            return PostAsync<AccountDto, int>("/V2/SaveAccount", accountDto);
        }

        public Task<int> CreateAccountAsync(int firmId, int userId, AccountDto accountDto)
        {
            return PostAsync<AccountDto, int>($"/V2/CreateAccount?firmId={firmId}&userId={userId}", accountDto);
        }

        public Task<MergeAccountsResultDto> MergeAccountsAsync(MergeAccountsRequestDto requestDto)
        {
            return PostAsync<MergeAccountsRequestDto, MergeAccountsResultDto>("/V2/MergeAccounts", requestDto);
        }
    }
}