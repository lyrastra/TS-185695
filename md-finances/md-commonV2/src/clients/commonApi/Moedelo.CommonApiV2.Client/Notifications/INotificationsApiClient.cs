using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Notifications;
using Moedelo.CommonApiV2.Dto.Notifications;

namespace Moedelo.CommonApiV2.Client.Notifications;

public interface INotificationsApiClient
{
    Task SaveAsync(int firmId, int userId, NotificationDataDto dto);

    Task<NotificationDto> GetAsync(int count = 10, int offset = 0);

    Task<List<NotificationDto>> GetPagedListAsync(int count = 10, int offset = 0, bool isForManyUsers = false);

    Task<NotificationDto> GetByIdAsync(int id);

    Task<int> GetCountByStatusesAsync(IList<NotificationStatus> statuses);

    Task<List<UserNotificationDto>> GetUserNotificationsAsync(int notificationId);

    Task<int> GetNotificationUsersCountAsync(int notificationId);

    Task ApproveAsync(int firmId, int userId, IList<int> notificationIds);

    Task DeleteAsync(int firmId, int userId, IList<int> notificationIds);
}