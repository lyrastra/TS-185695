using Moedelo.AccountV2.Client.Firm;
using Moedelo.Common.Enums.Enums.Notifications;
using Moedelo.CommonApiV2.Client.Notifications;
using Moedelo.CommonApiV2.Dto.Notifications;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationNotificationSender : IReconciliationNotificationSender
    {
        private readonly IFirmApiClient firmApiClient;
        private readonly INotificationsApiClient notificationsApiClient;
        private readonly IReconcilationFinishedIndicator finishedIndicator;

        public ReconciliationNotificationSender(
            IFirmApiClient firmApiClient,
            INotificationsApiClient notificationsApiClient,
            IReconcilationFinishedIndicator finishedIndicator)
        {
            this.firmApiClient = firmApiClient;
            this.notificationsApiClient = notificationsApiClient;
            this.finishedIndicator = finishedIndicator;
        }

        public Task SendSuccesNotificationAsync(int firmId, int userId, Guid sessionId)
        {
            var notification = new NotificationDto
            {
                Title = "Сверка с банком",
                Text = "Сверка раздела \"Деньги\" завершена успешно.",
                UrlTitle = "Посмотреть",
                Url = $"/Finances?_companyId={firmId}#viewReconciliationResults/{sessionId}",
                CreateDate = DateTime.Now,
                ShowDate = DateTime.Now,
                NotificationType = NotificationType.Info,
                RecipientType = RecipientType.Accounting,
                CreateUserId = userId,
                ApproveUserId = userId,
            };
            return SendAsync(firmId, userId, notification);
        }

        public Task SendNoDiffNotificationAsync(int firmId, int userId)
        {
            var notification = new NotificationDto
            {
                Title = "Сверка с банком",
                Text = $"Сверка раздела \"Деньги\" не выявила различий.",
                CreateDate = DateTime.Now,
                ShowDate = DateTime.Now,
                NotificationType = NotificationType.Info,
                RecipientType = RecipientType.Accounting,
                CreateUserId = userId,
                ApproveUserId = userId,
            };
            return SendAsync(firmId, userId, notification);
        }

        public Task SendErrorNotificationAsync(int firmId, int userId, string message)
        {
            var notification = new NotificationDto
            {
                Title = "Сверка с банком",
                Text = $"Сверка раздела \"Деньги\" завершена c ошибкой: {message}.",
                CreateDate = DateTime.Now,
                ShowDate = DateTime.Now,
                NotificationType = NotificationType.Info,
                RecipientType = RecipientType.Accounting,
                CreateUserId = userId,
                ApproveUserId = userId,
            };
            return SendAsync(firmId, userId, notification);
        }

        private async Task SendAsync(int firmId, int userId, NotificationDto notification)
        {
            var legalUserId = await firmApiClient.GetLegalUserId(firmId).ConfigureAwait(false);
            var notificationData = new NotificationDataDto
            {
                Notification = notification,
                UserNotifications = new List<UserNotificationDto>
                {
                    new UserNotificationDto
                    {
                        FirmId = firmId,
                        UserId = legalUserId,
                        Status = UserNotificationStatus.New
                    }
                }
            };
            await notificationsApiClient.SaveAsync(firmId, userId, notificationData).ConfigureAwait(false);
            await finishedIndicator.IdicatorOnAsync(firmId, new ReconcilationFinishedIndicatorData 
                                                    { Text = notificationData.Notification.Text, 
                                                      Url = notificationData.Notification.Url }).ConfigureAwait(false);
        }
    }
}
