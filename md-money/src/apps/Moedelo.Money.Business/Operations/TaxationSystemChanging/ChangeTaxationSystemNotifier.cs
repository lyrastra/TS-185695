using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications.Dtos;
using Moedelo.CommonApi.Enums.Notifications;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Operations
{
    [InjectAsSingleton(typeof(ChangeTaxationSystemNotifier))]
    class ChangeTaxationSystemNotifier
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly INotificationsApiClient notificationsApiClient;

        public ChangeTaxationSystemNotifier(
            IExecutionInfoContextAccessor contextAccessor,
            INotificationsApiClient notificationsApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.notificationsApiClient = notificationsApiClient;
        }

        public Task NotifyAsync(Guid guid)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var request = new NotificationSaveRequestDto
            {
                Notification = new NotificationDto
                {
                    Title = "Смена СНО",
                    Text = "Смена СНО в разделе \"Деньги\" завершена успешно",
                    CreateDate = DateTime.Now,
                    ShowDate = DateTime.Now,
                    NotificationType = NotificationType.Info,
                    RecipientType = NotificationRecipientType.List,
                    CreateUserId = (int)context.UserId,
                    ApproveUserId = (int)context.UserId,
                },
                UserNotifications = new List<UserNotificationDto>
                {
                    new UserNotificationDto
                    {
                        FirmId = (int)context.FirmId,
                        UserId = (int)context.UserId,
                        Status = UserNotificationStatus.New
                    }
                }
            };
            return notificationsApiClient.SaveAsync(context.FirmId, context.UserId, request);
        }
    }
}
