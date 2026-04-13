using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IAccountClient))]
    internal sealed class AccountClient : BaseApiClient, IAccountClient
    {
        public AccountClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AccountClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AccountPrivateApiEndpoint"),
                logger)
        {
        }

        public Task<AccountDto> GetAccountByUserIdAsync(int userId)
        {
            return GetAsync<AccountDto>("/Rest/Account/V2/GetByUserId", new { userId });
        }

        public Task<Dictionary<int, AccountDto>> GetByUserIdsAsync(IReadOnlyCollection<int> userIds)
        {
            return PostAsync< IReadOnlyCollection<int>, Dictionary<int, AccountDto>>("/Rest/Account/V2/GetByUserIds", userIds);
        }

        public Task<List<AccountDto>> GetByIdsAsync(IReadOnlyCollection<int> accountIds)
        {
            if (accountIds?.Any() != true)
            {
                return Task.FromResult(new List<AccountDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<AccountDto>>("/Rest/Account/V2/GetByIds", accountIds);
        }

        public Task<AccountFirmsDto> GetUserFirmsInfoByAccountId(int accountId)
        {
            return GetAsync<AccountFirmsDto>("/Rest/AccountFirms/GetAdminAndFirmsInAccount", new { accountId });
        }
    }
}
