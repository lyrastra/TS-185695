using Moedelo.CommonApi.Enums.Notifications;
using System;

namespace Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string UrlTitle { get; set; }

        public string Url { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ShowDate { get; set; }

        public int CreateUserId { get; set; }

        public int? ApproveUserId { get; set; }

        public NotificationType NotificationType { get; set; }

        public NotificationRecipientType RecipientType { get; set; }
    }
}
