using System;

namespace Moedelo.SystemNotifications.Dto
{
    /// <summary> Уведомление </summary>
    public class SystemNotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CreateUserId { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyUserId { get; set; }
        public bool IsVisible { get; set; }
        public bool IsClosable { get; set; }
        public int TotalRecipients { get; set; }
    }
}
