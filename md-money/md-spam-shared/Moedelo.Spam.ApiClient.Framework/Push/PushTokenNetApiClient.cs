using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushToken;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.Push;

namespace Moedelo.Spam.ApiClient.Framework.Push
{
    [InjectAsSingleton(typeof(IPushTokenNetApiClient))]
    internal sealed class PushTokenNetApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : BaseApiClient(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager), IPushTokenNetApiClient
    {
        private const string ApiRoute = "/api/v1/PushToken";

        private readonly SettingValue ApiEndPoint = settingRepository.Get("PushNetApiEndpoint");

        public Task SaveAsync(
            PushTokenSaveRequestDto dto,
            CancellationToken cancellationToken)
        {
            return PostAsync($"{ApiRoute}/Save", dto, cancellationToken: cancellationToken);
        }

        protected override string GetApiEndpoint()
        {
            return ApiEndPoint.Value;
        }
    }
}