using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Dto.Users;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Accounts.Clients.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Accounts.Clients
{
    [InjectAsSingleton(typeof(IUsersApiClient))]
    internal sealed class UsersApiClient : BaseLegacyApiClient, IUsersApiClient
    {
        public UsersApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UserClient> logger)
            : base(
                    httpRequestExecuter,
                    uriCreator,
                    auditTracer,
                    auditHeadersGetter,
                    settingRepository.Get("UserContextNetCoreApiEndpoint"),
                    logger)
        {
        }

        public async Task<IReadOnlyCollection<UserDto>> SearchAsync(SearchUserRequestDto requestDto)
        {
            var uri = $"/v1/Users/Search";
            var response = await PostAsync<SearchUserRequestDto, DataWrapper<IReadOnlyCollection<UserDto>>>(uri, requestDto);
            return response.Data;
        }
    }
}