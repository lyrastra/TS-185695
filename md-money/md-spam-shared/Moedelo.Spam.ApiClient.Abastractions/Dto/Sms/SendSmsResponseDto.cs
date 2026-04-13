namespace Moedelo.Spam.ApiClient.Abastractions.Dto.Sms
{
    public class SendSmsResponseDto
    {
        /// <summary>
        /// Идентификатор записи в SmsHistory
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Идентификатор отложенного сообщения в DeferredSms
        /// </summary>
        public int? DeferredSmsId { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}