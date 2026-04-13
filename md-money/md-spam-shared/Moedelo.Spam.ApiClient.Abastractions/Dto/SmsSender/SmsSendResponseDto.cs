namespace Moedelo.Spam.ApiClient.Abastractions.Dto.SmsSender
{
    public class SmsSendResponseDto
    {
        public string Number { get; set; }

        public string Text { get; set; }

        /// <summary> Status true when bytehand return status = 0, status mean success</summary>
        public bool Status { get; set; }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public string Description { get; set; }
    }
}