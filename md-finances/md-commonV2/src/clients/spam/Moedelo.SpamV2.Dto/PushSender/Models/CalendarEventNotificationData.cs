using System.ComponentModel.DataAnnotations;

namespace Moedelo.SpamV2.Dto.PushSender.Models
{
    public class CalendarEventNotificationData: IPushNotificationData
    {
        [MaxLength(600)]
        [Required(AllowEmptyStrings = false)]
        public string Message { get; set; }
    }
}
