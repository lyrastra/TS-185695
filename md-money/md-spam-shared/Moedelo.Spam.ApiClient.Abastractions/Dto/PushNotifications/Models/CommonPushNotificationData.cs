using System.ComponentModel.DataAnnotations;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models
{
    public class CommonPushNotificationData : IPushNotificationData
    {
        [MaxLength(400)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [MaxLength(600)]
        [Required(AllowEmptyStrings = false)]
        public string Message { get; set; }

        public object ExtraData { get; set; }
    }
}
