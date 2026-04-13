using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications.Dtos;
using Moedelo.CommonApi.Enums.Notifications;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Rule;

namespace Moedelo.Money.Business.Operations
{
    [InjectAsSingleton(typeof(UpdateExistOperationsNotifier))]
    internal sealed class UpdateExistOperationsNotifier
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly INotificationsApiClient notificationsApiClient;
        private readonly IImportRuleApiClient importRuleApiClient;

        public UpdateExistOperationsNotifier(
            IExecutionInfoContextAccessor contextAccessor,
            INotificationsApiClient notificationsApiClient,
            IImportRuleApiClient importRuleApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.notificationsApiClient = notificationsApiClient;
            this.importRuleApiClient = importRuleApiClient;
        }

        public async Task NotifyAsync(int importRuleId, int documentBaseIdsCount)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var request = new NotificationSaveRequestDto
            {
                Notification = new NotificationDto
                {
                    Title = "Обновление существующих операций",
                    Text = await GetTitleAsync(importRuleId, documentBaseIdsCount),
                    CreateDate = DateTime.Now,
                    ShowDate = DateTime.Now,
                    NotificationType = NotificationType.Info,
                    RecipientType = NotificationRecipientType.Accounting,
                    CreateUserId = (int) context.UserId,
                    ApproveUserId = (int) context.UserId
                },
                UserNotifications = new List<UserNotificationDto>
                {
                    new UserNotificationDto
                    {
                        FirmId = (int) context.FirmId,
                        UserId = (int) context.UserId,
                        Status = UserNotificationStatus.New
                    }
                }
            };

            await notificationsApiClient.SaveAsync(context.FirmId, context.UserId, request);
        }

        private async Task<string> GetTitleAsync(int importRuleId, int documentBaseIdsCount)
        {
            var importRule = await importRuleApiClient.GetAsync(importRuleId);
            var ruleName = importRule?.Name ?? string.Empty;

            return $"Правило {ruleName} успешно применено на {documentBaseIdsCount} операций.";
        }
    }
}
