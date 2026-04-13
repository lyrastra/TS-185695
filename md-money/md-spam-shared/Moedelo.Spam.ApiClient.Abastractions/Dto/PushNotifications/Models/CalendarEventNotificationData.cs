using System.ComponentModel.DataAnnotations;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models
{
    public class CalendarEventNotificationData: IPushNotificationData
    {
        [MaxLength(600)]
        [Required(AllowEmptyStrings = false)]
        public string Message { get; set; }
    }
}
