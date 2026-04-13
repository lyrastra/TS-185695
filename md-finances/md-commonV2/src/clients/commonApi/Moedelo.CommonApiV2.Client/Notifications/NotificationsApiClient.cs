using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Notifications;
using Moedelo.CommonApiV2.Dto.Notifications;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonApiV2.Client.Notifications;

[InjectAsSingleton(typeof(INotificationsApiClient))]
internal sealed class NotificationsApiClient : BaseApiClient, INotificationsApiClient
{
    private readonly SettingValue apiEndPoint;
        
    public NotificationsApiClient(IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) 
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("CommonApiPrivateApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public Task SaveAsync(int firmId, int userId, NotificationDataDto dto)
    {
        return PostAsync($"/Notifications/Save?firmId={firmId}&userId={userId}", dto);
    }

    public Task<NotificationDto> GetAsync(int count = 10, int offset = 0)
    {
        return GetAsync<NotificationDto>($"/Notifications/Get?count={count}&offset={offset}");
    }

    public Task<List<NotificationDto>> GetPagedListAsync(int count = 10, int offset = 0, bool isForManyUsers = false)
    {
        return GetAsync<List<NotificationDto>>($"/Notifications/GetPagedList?count={count}&offset={offset}&isForManyUsers={isForManyUsers}");
    }

    public Task<NotificationDto> GetByIdAsync(int id)
    {
        return GetAsync<NotificationDto>($"/Notifications/GetById?id={id}");
    }

    public Task<int> GetCountByStatusesAsync(IList<NotificationStatus> statuses)
    {
        return PostAsync<IList<NotificationStatus>, int>("/Notifications/Get", statuses);
    }

    public Task<List<UserNotificationDto>> GetUserNotificationsAsync(int notificationId)
    {
        return GetAsync<List<UserNotificationDto>>($"/Notifications/GetUserNotifications?notificationId={notificationId}");
    }

    public Task<int> GetNotificationUsersCountAsync(int notificationId)
    {
        return GetAsync<int>($"/Notifications/GetNotificationUsersCount?notificationId={notificationId}");
    }

    public Task ApproveAsync(int firmId, int userId, IList<int> notificationIds)
    {
        return PostAsync($"/Notifications/Approve?firmId={firmId}&userId={userId}", notificationIds);
    }

    public Task DeleteAsync(int firmId, int userId, IList<int> notificationIds)
    {
        return PostAsync($"/Notifications/Delete?firmId={firmId}&userId={userId}", notificationIds);
    }
}
