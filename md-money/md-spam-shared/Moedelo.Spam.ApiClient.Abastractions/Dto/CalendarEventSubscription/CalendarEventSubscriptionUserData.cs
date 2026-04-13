namespace Moedelo.Spam.ApiClient.Abastractions.Dto.CalendarEventSubscription
{
    public class CalendarEventSubscriptionUserData
    {
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public string PhoneNumber { get; set; }

        public bool NoticeByMail { get; set; }

        public bool NoticeBySms { get; set; }
    }
}