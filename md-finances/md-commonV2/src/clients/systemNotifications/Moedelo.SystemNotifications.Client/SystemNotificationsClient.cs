using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SystemNotifications.Dto;

namespace Moedelo.SystemNotifications.Client
{
    [InjectAsSingleton]
    public class SystemNotificationsClient : BaseCoreApiClient, ISystemNotificationsClient
    {
        private readonly SettingValue apiEndpoint;

        public SystemNotificationsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("SystemNotificationsEndpoint");
        }

        public async Task<SystemNotificationEditDto> GetByIdAsync(int firmId, int userId, int id)
        {
            var auth = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await GetAsync<ApiResponse<SystemNotificationEditDto>>($"{id}", null, auth).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<QueryListResult<SystemNotificationDto>> GetByCriteriaAsync(int firmId, int userId, NotificationsQueryDto dto)
        {
            var auth = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            return await GetAsync<QueryListResult<SystemNotificationDto>>(
                "GetForPartner", dto, auth).ConfigureAwait(false);
        }

        public async Task<int> CreateNotificationAsync(int firmId, int userId, SystemNotificationEditDto notification)
        {
            var auth = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            return await PostAsync<SystemNotificationEditDto, int>("", notification, auth).ConfigureAwait(false);
        }

        public async Task UpdateNotificationAsync(int firmId, int userId, SystemNotificationEditDto notification)
        {
            var auth = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            await PutAsync("", notification, auth).ConfigureAwait(false);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/private/api/v1/Notification/";
        }

        private class ApiResponse<T>
        {
            public T Data { get; set; }
        }
    }
}
