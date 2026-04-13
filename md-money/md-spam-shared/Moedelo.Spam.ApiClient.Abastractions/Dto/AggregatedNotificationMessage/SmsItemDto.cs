namespace Moedelo.Spam.ApiClient.Abastractions.Dto.AggregatedNotificationMessage
{
    public class SmsItemDto
    {
        public string MessageText { get; set; }
        public string PhoneNumber { get; set; }
        public string SentFromAppModule { get; set; }
        public string WlSpecialParams { get; set; }
    }
}
