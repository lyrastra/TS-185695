#nullable enable
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.UserContext;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Exceptions.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.UserContext
{
    [InjectAsSingleton(typeof(IUserContextNetCoreApiClient))]
    public sealed class UserContextNetCoreApiClient : BaseCoreApiClient, IUserContextNetCoreApiClient
    {
        private readonly SettingValue endpoint;

        public UserContextNetCoreApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            settingRepository,
            auditTracer,
            auditScopeManager)
        {
            endpoint = settingRepository.Get("UserContextNetCoreApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public Task<UserContextBasicInfoDto> GetBasicInfoAsync(int userId, int firmId)
        {
            var url = $"/V1/BasicInfo?userId={userId}&firmId={firmId}";

            return GetAsync<UserContextBasicInfoDto>(url);
        }

        public async Task<UserContextBasicInfoDto?> GetAuthorizedUserInfoAsync(int userId, int firmId,
            CancellationToken cancellationToken)
        {
            var url = $"/V1/BasicInfo?userId={userId}&firmId={firmId}";

            try
            {
                return await GetAsync<UserContextBasicInfoDto>(url, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (HttpRequestResponseStatusException exception)
                when (exception.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }
        }
    }
}
