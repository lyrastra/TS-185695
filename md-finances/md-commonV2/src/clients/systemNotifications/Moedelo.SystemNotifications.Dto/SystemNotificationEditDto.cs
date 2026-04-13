using System;
using System.Collections.Generic;

namespace Moedelo.SystemNotifications.Dto
{
    public class SystemNotificationEditDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя кто создал
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id фирмы пользователя кто создал
        /// </summary>
        public int FirmId { get; set; }

        public string Text { get; set; }
        public string Title { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsVisible { get; set; }
        public bool IsClosable { get; set; }
        public List<int> Users { get; set; }
    }
}
